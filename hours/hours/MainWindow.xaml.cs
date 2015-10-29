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


namespace hours
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*
            pouzito z INFO
        */
        private System.Windows.Threading.DispatcherTimer timerCas;

        public MainWindow()
        {
            InitializeComponent();
            System.Console.WriteLine("~app started!");
            this.ShowInTaskbar = false;
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            this.Top = 0;
            Info informacie = new Info();

            /* treba pozdeji zmenit :) pocasi se nacte az pozdeji po spusteni */
            while (informacie.teplota == null) {; }
           

            /*
             Casovac
            */
            this.timerCas = new System.Windows.Threading.DispatcherTimer();
            this.timerCas.Tick += new EventHandler(nastavCas);
            this.timerCas.Interval = new TimeSpan(0, 0, 1); //1 sekunda
            this.timerCas.Start();

            /* pokus */
            okno.Width = okno.Height = 400;
            kolecko.Width = okno.Width - 50;
            kolecko.Height = okno.Height - 50;
            sekundaRucicka.Height = (kolecko.Height/2) - (kolecko.Height/16);
            minutaRucicka.Height = (kolecko.Height/2) - (kolecko.Height/8);
            hodinaRucicka.Height = (kolecko.Height/2) - (kolecko.Height/4);

            /* sekce pocasi */
            teplota.Content = informacie.teplota + " °C";
            lokace.Content = informacie.lokacia;
            System.Console.WriteLine(informacie.obrazokURL);
            //obrazekPocasi.Source = http://www.icons.wxug.com/i/c/k/cloudy.gif;
        }

        private void nastavCas(object sender, EventArgs e)
        {
            sekunda.Angle = DateTime.Now.Second * 6;
            minuta.Angle = DateTime.Now.Minute * 6;
            hodina.Angle = DateTime.Now.Minute * 0.5 + DateTime.Now.Hour * 30;

            digitalTime.Content = DateTime.Now.Hour.ToString() + "h " + DateTime.Now.Minute.ToString() + "m " + DateTime.Now.Second.ToString() + "s";
            datum.Content = DateTime.Now.DayOfWeek + "  " + DateTime.Now.Day + ". " + DateTime.Now.Month + ". " + DateTime.Now.Year;
        }


        /*
         * Metoda, ktora sa stara aby sa dalo drag and drop pohybovat okno
         */
        private void dragAndDropAplikacie(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        /*
         * Metoda, tkora sa stara aby bolo okno stale navrchu vsetkeho
         */
        private void staleNavrchu(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Topmost = true;
        }
    }
}
