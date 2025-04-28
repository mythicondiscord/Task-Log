using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace TaskApp
{
    public partial class MainWindow : Window
    {
        static string solutionDirectory = string.Empty;
        static string solutionDataPath = string.Empty;

        public MainWindow()
        {
            // Init
            InitializeComponent();

            // Init Fields
            solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.FullName!;
            solutionDataPath = Path.Combine(solutionDirectory, "Data");

            this.Topmost = true;

            // Extra
            var dataFiles = Directory.GetDirectories(solutionDataPath);

            foreach (var dataDirectory in dataFiles)
            {
                if (Directory.Exists(dataDirectory))
                {
                    string dataDirectoryName = Path.GetFileName(dataDirectory)!;
                    TaskComboBox.Items.Add(dataDirectoryName);
                }
             }
        }
        
        static void OpenTaskWindow(string taskPath)
        {
            // New Window
            TaskWindow newWindow = new TaskWindow();
            newWindow.Title = Path.GetFileName(taskPath);
            newWindow.Topmost = true;
            newWindow.Show();

            // Extra
            Application.Current.MainWindow.Close();
        }

        private void OpenTaskLog_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = TaskComboBox.SelectedIndex;
            var dataFiles = Directory.GetDirectories(solutionDataPath);

            OpenTaskWindow(dataFiles[selectedIndex]);
        }
        
        private void DeleteTaskLog_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = TaskComboBox.SelectedIndex;
            var dataFiles = Directory.GetDirectories(solutionDataPath);

            if (selectedIndex != -1)
            {
                TaskComboBox.Items.RemoveAt(selectedIndex);
                Directory.Delete(dataFiles[selectedIndex], true);
            }
        }

        private void CreateTaskLog_Click(object sender, RoutedEventArgs e)
        {
            string logInput = TaskLogBox.Text;

            if (logInput.Length > 0)
            {
                string newLogPath = Path.Combine(solutionDataPath, logInput);

                if (!Directory.Exists(newLogPath))
                {
                    Directory.CreateDirectory(newLogPath);
                    //TaskComboBox.Items.Add(Path.GetFileName(newLogPath));
                    OpenTaskWindow(newLogPath);
                }
            };
        }
    }
}
