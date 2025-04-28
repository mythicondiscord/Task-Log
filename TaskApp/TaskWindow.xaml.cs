using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace TaskApp
{
    public partial class TaskWindow : Window
    {
        public enum ImportanceType { Crucial, Moderate, Low };

        public class Task
        {
            public string? TaskInfo { get; set; }
            public ImportanceType Importance { get; set; }
        }

        public TaskWindow()
        {
            InitializeComponent();

            List<Task> taskItems = new List<Task>();
            taskItems.Add(new Task() { TaskInfo = "John Doe", Importance = ImportanceType.Crucial });
            taskItems.Add(new Task() { TaskInfo = "Jane Doe", Importance = ImportanceType.Moderate });
            taskItems.Add(new Task() { TaskInfo = "Sammy Doe", Importance = ImportanceType.Low });
            listViewTasks.ItemsSource = taskItems;

            CollectionView view = (CollectionView) CollectionViewSource.GetDefaultView(listViewTasks.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Importance");
            view.GroupDescriptions.Add(groupDescription);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow newWindow = new MainWindow();
            Application.Current.MainWindow = newWindow;
            newWindow.Show();
        }
    }
}
