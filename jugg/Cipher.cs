using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jugg
{
    public class Cipher
    {
        public Cipher() { }
        private string source_text;
        private bool check;
        public string Source_text
        {
            get { return source_text; }
            set { source_text = value; }
        }
        public bool Check
        {
            get { return check; }
            set { check = value; }
        }
        public Cipher(string source_text, bool check)
        {
            this.source_text = source_text;
            this.check = check;
        }

        public string Atbash()
        {
            // 0 - А 
            string alhabet = check == true ? "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ" : "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string result = "";
            for (int i = 0; i < source_text.Length; i++)
            {
                if (!alhabet.Contains(source_text.ToUpper()[i]))
                    result += source_text[i];
                else if (Char.IsLower(source_text[i]))
                    result += alhabet.ToLower()[alhabet.Length - 1 - alhabet.IndexOf(source_text.ToUpper()[i])];
                else
                    result += alhabet[alhabet.Length - 1 - alhabet.IndexOf(source_text[i])];
            }
            return result;
        }
    }
}