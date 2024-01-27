using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Cryptography;

namespace jugg
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
   

    public partial class RegWindow : Window
    {
        AppContext db;
        public RegWindow()
        {
            InitializeComponent();

            db = new AppContext();
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

        private void Button_Register(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text.Trim();
            string password = textBoxPassword.Password.Trim();
            string password_2 = textBoxPasswordRepeat.Password.Trim();
            char [] uniqueSymbols = { '%', '*', '?', '@', '#', '$', '~', '!' };
            


            if (login.Length < 5)
            {
                textBoxLogin.ToolTip = "Необходимо ввести минимум 5 символов";
                textBoxLogin.BorderBrush = Brushes.Red;

            }
          
            else if (new AppContext().Users.Select(a => a.Login).Contains(textBoxLogin.Text))
            {
                textBoxLogin.ToolTip = "Данный логин занят другим пользователем";
                textBoxLogin.BorderBrush = Brushes.Red;
            }
            else if (password.Length < 8 || !password.Any(char.IsLetter) || !password.Any(ch => uniqueSymbols.Contains(ch)))
            {
                textBoxLogin.ToolTip = null;
                textBoxLogin.BorderBrush = Brushes.Green;
                textBoxPassword.ToolTip = "Ненадёжный пароль (минимум 8 символов). \nИспользуйте буквы и символы из спец. алфавита: %*?@#$~!";
                textBoxPassword.BorderBrush = Brushes.Red;
            }
            else if (password != password_2)
            {
                textBoxLogin.ToolTip = null;
                textBoxLogin.BorderBrush = Brushes.Green;
                textBoxPassword.ToolTip = null;
                textBoxPassword.BorderBrush = Brushes.Green;
                textBoxPasswordRepeat.ToolTip = "Пароли не совпадают";
                textBoxPasswordRepeat.BorderBrush = Brushes.Red;
            }

            else if (login.Contains("\\") || password.Contains("\\") || login.Contains("'") || password.Contains("'")) 
            {
                MessageBox.Show("Логин и пароль не могут содержать символы обратной косой черты (\\) или апострофа ('), пожалуйста, введите другие данные.");
            }


            else
            {
                User user = new User(login, HashPassword(password));
                db.Users.Add(user);
                db.SaveChanges();
               
                AuthWindow authWindow = new AuthWindow();
                authWindow.Show();
                this.Close();
            }


        }

        private void Button_Authorization(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }
    }


}
