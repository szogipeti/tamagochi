using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tamagochi.Model;

namespace Tamagochi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        laravelContext context = new laravelContext();
        User user = new ();
        public MainWindow()
        {
            InitializeComponent();
        }

       public void ShowUsers_Click(object sender, RoutedEventArgs e)
        {
            felhasznalok.Items.Clear();
            foreach (var item in context.Users)
            {
                felhasznalok.Items.Add(item);
            }
        }


        private void ShowUserData(User user)
        {
            sp_user.Visibility = Visibility.Visible;
            username.Content = $"Felhasználónév: {user.Username}" ;
            email.Content = $"Email cím: {user.Email}";
        }

        private async void NewAnimal_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "")
            {
                MessageBox.Show("Nem töltötte ki a mezőt!");
                return;
            }
            if(img_animal.Source == null)
            {
                MessageBox.Show("Nem töltött fel képet!");
                return;
            }

            BitmapImage image = img_animal.Source as BitmapImage;
            string imagePath = "img/" + image.UriSource.AbsolutePath.Split('/').Last();

            if(File.Exists(@"../../../../../tamagochi-laravel/storage/app/" + imagePath))
            {
                MessageBox.Show("Már létezik kép ilyen névvel!");
                return;
            }

            var json = JsonConvert.SerializeObject(new
            {
                name = name.Text,
                image = imagePath
            });
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:8881/api/animals", new StringContent(json, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    File.Copy(image.UriSource.AbsolutePath, "../../../../../tamagochi-laravel/storage/app/" + imagePath);
                    MessageBox.Show("Új állat létrehozva");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Error error = new Error(errorResponse);

                    List<string> errorMessages = new List<string>();
                    foreach (var errorMessage in error.Messages)
                    {
                        errorMessages.Add(errorMessage.ToString());
                    }
                    MessageBox.Show($"Sikertelen állat felvétel:\n{string.Join("\n\t", errorMessages)}");
                }
            }
        }

        class Error
        {
            public Error(string json)
            {
                JObject jObject = JObject.Parse(json);
                JToken jError = jObject["errors"];
                JToken[] nameErrors = new JToken[0];
                JToken[] imageErrors = new JToken[0];
                if (jError["name"] != null)
                {
                    nameErrors = jError["name"].ToArray();
                }
                if (jError["image"] != null)
                {
                    imageErrors = jError["image"].ToArray();
                }

                Messages = nameErrors.Union(imageErrors);

            }
            public IEnumerable Messages { get; set; }
        }
        private async void PasswordChancge_Click(object sender, RoutedEventArgs e)
        {
            var json = JsonConvert.SerializeObject(new { id= user.Id , new_password = password.Text });
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsync("http://localhost:8881/api/password", new StringContent(json, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Új jelszó beállítva! ");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Sikertelen jelszó csere: {errorResponse}");
                }
            }
        }


        private void felhasznalok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            user = ((ListBox)sender).SelectedItem as User;
            if (user == null)
            {
                return;
            }
            ShowUserData(user);
        }

        private void btn_imgSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Válaszd ki az állathoz tartozó képet!";
            openFileDialog.DefaultExt = "png";
            openFileDialog.Filter = "Image files|*.png;*.bpm;*.jpg";
            
            if(openFileDialog.ShowDialog() == true)
            {
                img_animal.Source = null;
                img_animal.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
    }
}
