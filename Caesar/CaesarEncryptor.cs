using System;
using System.Data;

namespace Caesar
{
    static class CaesarEncryptor
    {
        static readonly string english = "abcdefghijklmnopqrstuvwxyz";
        static readonly string ukrainian = "абвгґдеєжзиіїйклмнопрстуфхцчшщьюя";

        static public string Encrypt(string text, int key, int lang)
        {
            if (key == 0 || text.Length == 0)
                return text;

            string alphabet;
            if (lang == 0)
                alphabet = english;
            else
                alphabet = ukrainian;

            key %= alphabet.Length;

            int sign;
            if (key > 0)
                sign = -1;
            else
                sign = 1;
            string result = string.Empty;

            foreach (var c in text)
            {
                if (alphabet.Contains(char.ToLower(c)))
                {
                    int pos = alphabet.IndexOf(char.ToLower(c)) + key;
                    if (pos >= alphabet.Length || pos < 0)
                        pos += sign * alphabet.Length;

                    if (char.IsUpper(c))
                    {
                        result += char.ToUpper(alphabet[pos]);
                    }
                    else
                    {
                        result += alphabet[pos];
                    }
                }
                else
                {
                    result += c;
                }
            }
            return result;
        }

        static public string Decrypt(string text, int key, int lang)
        {
            key *= -1;
            return Encrypt(text, key, lang);
        }

        static public void BruteForce(string text, int lang)
        {
            DataTable dt = new DataTable();
            int mod;
            dt.Columns.Add(new DataColumn("Key", Type.GetType("System.Int32")));
            dt.Columns.Add(new DataColumn("Text variant", Type.GetType("System.String")));
            if (lang == 0)
            {
                mod = 26;
            }
            else
            {
                mod = 33;
            }
            for (int i = 0; i < mod; i++)
            {
                TryKey(text, i, lang, dt);
            }
            VariantsWindow vw = new VariantsWindow(dt);
            vw.Show();
        }

        static private void TryKey(string text, int key, int lang, DataTable dt)
        {
            DataRow dr = dt.NewRow();
            string res = Encrypt(text, key, lang);
            dr.ItemArray = new object[] { key, res };

            dt.Rows.Add(dr);
        }
    }
}
