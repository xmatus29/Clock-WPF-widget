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

namespace hours
{
    /// <summary>
    /// Interaction logic for Alarm.xaml
    /// </summary>
    public partial class Alarm : Window
    {
        public string file;
        public Alarm()
        {
            InitializeComponent();
           
            AlarmHodiny.Text = Properties.Settings.Default.alarmHodiny.ToString();
            AlarmMinuty.Text = Properties.Settings.Default.alarmMinuty.ToString();

            //AlarmHodiny.Text = System.DateTime.Now.Hour.ToString();
            //AlarmMinuty.Text = System.DateTime.Now.Minute.ToString();

            if (Properties.Settings.Default.alarm == false)
            {
                AlarmButton.Background = Brushes.Green;
                AlarmButton.Content = "ON";
            }
            else
            {
                AlarmButton.Background = Brushes.Red;
                AlarmButton.Content = "OFF";
            }

            if(Properties.Settings.Default.alarmMusic == "")
            {
                tema.Text = "Default";
            }
            else
            {
                tema.Text = Properties.Settings.Default.alarmMusic.Substring(Properties.Settings.Default.alarmMusic.LastIndexOf('-') + 1);
            }
        }

        /*
         * Akcia po spusteni/vypnuti alarmu
         */
        private void AlarmButton_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.alarm == true)
            {
                AlarmButton.Background = Brushes.Green;
                AlarmButton.Content = "ON";
                Properties.Settings.Default.alarm = false;
            }
            else
            {
                AlarmButton.Background = Brushes.Red;
                AlarmButton.Content = "OFF";
                Properties.Settings.Default.alarm = true;
            }
        }

        private void SetAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (char c in (AlarmMinuty.Text.ToString()))
            {
                if (c < '0' || c > '9')
                    return;
            }
            if (string.Compare(AlarmMinuty.Text.ToString(), "") == 0)
            {
                return;
            }
            if (Convert.ToInt32(AlarmMinuty.Text.ToString()) > 59 || Convert.ToInt32(AlarmMinuty.Text.ToString()) < 0)
            {
                return;
            }
            Properties.Settings.Default.alarmMinuty = AlarmMinuty.Text.ToString();

            foreach (char c in (AlarmHodiny.Text.ToString()))
            {
                if (c < '0' || c > '9')
                    return;
            }
            if (string.Compare(AlarmHodiny.Text.ToString(), "") == 0)
            {
                return;
            }
            if (Convert.ToInt32(AlarmHodiny.Text.ToString()) > 24 || Convert.ToInt32(AlarmHodiny.Text.ToString()) < 0)
            {
                return;
            }
            Properties.Settings.Default.alarmHodiny = AlarmHodiny.Text.ToString();
        }

        private void selectMusic_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();


            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".mp3";
            dlg.Filter = "mp3 Files (*.mp3)|*.mp3|wav files (*.wav)|*.wav";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                //string filename = dlg.FileName;
                Properties.Settings.Default.alarmMusic = dlg.FileName;
                tema.Text = Properties.Settings.Default.alarmMusic.Substring(Properties.Settings.Default.alarmMusic.LastIndexOf('-') + 1);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            MainWindow.I.alarm = null;
        }
    }
}
