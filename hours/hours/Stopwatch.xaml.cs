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
            StopkyZobrazovanie.Text = "00:00:000";
            StartStopkyButton.Background = Brushes.Green;
            StartStopkyButton.Content = "START";
            this.cas.Reset();
        }

        private void count(object sender, EventArgs e)
        {
            if(this.cas.IsRunning)
            {
                string minuty = this.cas.Elapsed.Minutes.ToString();
                string sekundy = this.cas.Elapsed.Seconds.ToString();
                string milisekundy = this.cas.Elapsed.Milliseconds.ToString();
                if(minuty.Length < 2)
                {
                    minuty = minuty.Insert(0, "0");
                }
                if (sekundy.Length < 2)
                {
                    sekundy = sekundy.Insert(0, "0");
                }
                if (milisekundy.Length == 1)
                {
                    milisekundy = milisekundy.Insert(0, "00");
                }
                else if (milisekundy.Length == 2)
                {
                    milisekundy = milisekundy.Insert(0, "0");
                }
                StopkyZobrazovanie.Text = minuty + ":" + sekundy + ":"  + milisekundy;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            MainWindow.I.stopky = null;
        }
    }
}
