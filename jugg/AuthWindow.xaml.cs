using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace jugg
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private void Button_Input(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string password = HashPassword(textBoxPassword.Password.Trim());

            if (!(new AppContext().Users.Any(a => a.Login == login)))
            {
                textBoxLogin.ToolTip = "Неверный логин";
                textBoxLogin.BorderBrush = Brushes.Red;

            }
            else if (!(new AppContext().Users.Any(a => a.Password == password)))
            {
                textBoxLogin.ToolTip = null;
                textBoxLogin.BorderBrush = Brushes.Green;
                textBoxPassword.ToolTip = "Неверный пароль";
                textBoxPassword.BorderBrush = Brushes.Red;
            }

            else if (login.Contains("\\") || password.Contains("\\") || login.Contains("'") || password.Contains("'")) 
            {
                MessageBox.Show("Логин и пароль не могут содержать символ обратной косой черты (\\) или апострофа ('), пожалуйста, введите другие данные.");
            }

            else
            {
                User authUser = null;
                using(AppContext db = new AppContext())
                {
                    authUser = db.Users.Where(a => a.Login == login && a.Password == password).FirstOrDefault();
                }

                if (authUser != null)
                {
                    UserPageWindow userPageWindow = new UserPageWindow();
                    userPageWindow.Show();
                    this.Close();
                }
                else
                {
                    textBoxLogin.ToolTip = "Неверный логин или пароль";
                    textBoxLogin.BorderBrush = Brushes.Red;
                    textBoxPassword.ToolTip = "Неверный логин или пароль";
                    textBoxPassword.BorderBrush = Brushes.Red;
                }
            }

        }

        private void Button_Registration(object sender, RoutedEventArgs e)
        {
            RegWindow regWindow = new RegWindow();
            regWindow.Show();
            this.Close();
        }

        private void textBoxLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
