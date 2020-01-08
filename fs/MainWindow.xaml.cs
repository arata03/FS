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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace fs
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        bool result;
        bool fOptionBox = false;
        string str;

        public MainWindow()
        {
            InitializeComponent();
            Environment.CurrentDirectory = Directory.GetCurrentDirectory();
            string[] filePaths = Directory.GetFiles(Environment.CurrentDirectory, "*", SearchOption.AllDirectories);
            foreach (string filePath in filePaths)
            {
                result = Regex.IsMatch(filePath, ".(exe|docx|xlsx|vsdx)$");
                if (result) listBox.Items.Add(filePath);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(fileBox.Text, optionBox.Text);
            }
            catch (Win32Exception)
            {
                MessageBox.Show("関連付けられているファイルを開いているときにエラーが発生しました。", "ERROR", MessageBoxButton.OKCancel, MessageBoxImage.Error);
            }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!fOptionBox)
            {
                optionBox.Text = null;
                fOptionBox = true;
            }
            str = listBox.SelectedItem.ToString();
            str = System.IO.Path.GetFileName(str);
            fileBox.Text = str;

        }
    }
}
