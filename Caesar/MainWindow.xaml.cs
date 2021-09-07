using Microsoft.Win32;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace Caesar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
                fileTB.Text = File.ReadAllText(openFileDialog.FileName);
        }

        private void shift_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void saveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, fileTB.Text);
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void about_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Created by\nihornekhaienko\non 01.24.21");
        }

        private void encrypt_Click(object sender, RoutedEventArgs e)
        {
            int key = Convert.ToInt32(keyTB.Text);

            if (rightRB?.IsChecked ?? false)
                key *= -1;
            string text = fileTB.Text;

            fileTB.Text = CaesarEncryptor.Encrypt(text, key, engRB.IsChecked ?? false ? 0 : 1);
        }

        private void decrypt_Click(object sender, RoutedEventArgs e)
        {
            string text = fileTB.Text;
            int key = Convert.ToInt32(keyTB.Text);

            fileTB.Text = CaesarEncryptor.Decrypt(text, key, engRB?.IsChecked ?? false ? 0 : 1);
        }

        private void bf_Click(object sender, RoutedEventArgs e)
        {
            string text = fileTB.Text;
            if (text.Length == 0)
                return;

            CaesarEncryptor.BruteForce(text, engRB?.IsChecked ?? false ? 0 : 1);
        }
    }
}
