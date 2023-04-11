using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

            var json = JsonConvert.SerializeObject(new { name = name.Text });
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.PostAsync("http://localhost:8881/api/animals", new StringContent(json, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Új állat létrehozva");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Sikertelen állat felvétel: {errorResponse}");
                }
            }
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
    }
}
