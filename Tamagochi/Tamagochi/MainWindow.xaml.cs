using System;
using System.Collections.Generic;
using System.Linq;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            felhasznalok.Items.Clear();
            foreach (var item in context.Users)
            {
                felhasznalok.Items.Add(item);
            }
        }

        private void SelectedUser(object sender, SelectionChangedEventArgs e)
        {
            user = ((ListBox)sender).SelectedItem as User;
            if (user == null)
            {
                return;
            }
            ShowUserData(user);
        }

        private void ShowUserData(User user)
        {
            username.Content = $"Felhasználónév: {user.Username}" ;
            email.Content = $"Email cím: {user.Email}";
            password.Content = $"Felhasználónév: {user.Password}";

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
                
        }
    }
}
