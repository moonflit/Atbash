using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для UserPageWindow.xaml
    /// </summary>
    public partial class UserPageWindow : Window
    {
        public UserPageWindow()
        {
            InitializeComponent();
        }

        private void Button_Execut(object sender, RoutedEventArgs e)
        {
            string source_text = sourceText.Text; // исходный текст
            string alphabet = Alphabet.Text; // русский = тру, английский = фолс;
            string result = "";
            if (!String.IsNullOrEmpty(source_text))
            {
                Cipher cipher = null;
                if (alphabet == "Русский")
                {
                    cipher = new Cipher(source_text, true);
                }
                else if (alphabet == "Английский")
                {
                    cipher = new Cipher(source_text, false);
                }
                else
                {
                    Alphabet.ToolTip = "Выберите язык";
                    Alphabet.BorderBrush = Brushes.Red;
                }
                if (cipher != null)
                {
                    Alphabet.ToolTip = "";
                    Alphabet.BorderBrush = Brushes.Gray;
                    result = cipher.Atbash();
                }

                outputText.Visibility = Visibility.Visible;
                outputText.Text = result;
                outputText.BorderBrush = Brushes.White;
                outputText.Foreground = Brushes.White; 
            }
        }

        private void sourceText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
