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
    /// Interaction logic for AutoTurn.xaml
    /// </summary>
    public partial class AutoTurn : Window
    {
        public AutoTurn()
        {
            InitializeComponent();
            AlarmHodiny.Text = Properties.Settings.Default.autoHodiny.ToString();
            AlarmMinuty.Text = Properties.Settings.Default.autoMinuty.ToString();
            if (Properties.Settings.Default.auto == false)
            {
                AlarmButton.Background = Brushes.Green;
                AlarmButton.Content = "ON";
            }
            else
            {
                AlarmButton.Background = Brushes.Red;
                AlarmButton.Content = "OFF";
            }
        }

        private void AlarmButton_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.auto == true)
            {
                AlarmButton.Background = Brushes.Green;
                AlarmButton.Content = "ON";
                Properties.Settings.Default.auto = false;
            }
            else
            {
                AlarmButton.Background = Brushes.Red;
                AlarmButton.Content = "OFF";
                Properties.Settings.Default.auto = true;
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
            Properties.Settings.Default.autoMinuty = AlarmMinuty.Text.ToString();

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
            Properties.Settings.Default.autoHodiny = AlarmHodiny.Text.ToString();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            MainWindow.I.auto = null;
        }
    }
}
