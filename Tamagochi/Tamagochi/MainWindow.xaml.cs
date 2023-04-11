using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        private void ShowUsers_Click(object sender, RoutedEventArgs e)
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
            var newanimal = "";
            if (name.Text == "")
            {
                MessageBox.Show("Nem töltötte ki a mezőt!");
            }
            else
            {
                 newanimal = name.Text;
            }
            var json = JsonConvert.SerializeObject(newanimal);
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync("http://localhost:8881/api/animals", new StringContent(json, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Animal created successfully!");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error creating animal: {errorResponse}");
                }
            }
        }

        private void PasswordChancge_Click(object sender, RoutedEventArgs e)
        {

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
