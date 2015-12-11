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
    /// Interaction logic for Stopwatch.xaml
    /// </summary>
    public partial class Stopwatch : Window
    {
        public System.Diagnostics.Stopwatch cas;
        private System.Windows.Threading.DispatcherTimer timer;

        public Stopwatch()
        {
            InitializeComponent();
            this.cas = new System.Diagnostics.Stopwatch();
            this.timer = new System.Windows.Threading.DispatcherTimer();
            this.timer.Tick += new EventHandler(count);
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 1); //1 hodina
            this.timer.Start();
        }

        private void StartStopkyButton_Click(object sender, RoutedEventArgs e)
        {
            if(this.cas.IsRunning)
            {
                this.cas.Stop();
                StartStopkyButton.Background = Brushes.Green;
                StartStopkyButton.Content = "START";
            }
            else
            {
                this.cas.Start();
                StartStopkyButton.Background = Brushes.Red;
                StartStopkyButton.Content = "STOP";
            }
        }

        private void ResetStopkyButton_Click(object sender, RoutedEventArgs e)
        {
            StopkyZobrazovanie.Text = "00:00:00";
            this.cas.Reset();
        }

        private void count(object sender, EventArgs e)
        {
            if(this.cas.IsRunning)
            {
                StopkyZobrazovanie.Text = this.cas.Elapsed.Minutes + ":" + this.cas.Elapsed.Seconds + ":"  + this.cas.Elapsed.Milliseconds;
            }
        }
    }
}
