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
    /// 

    public partial class MainWindow : Window
    {
        
        private System.Windows.Threading.DispatcherTimer timerCas;
        public static MainWindow I;

        public MainWindow()
        {
            I = this;
            InitializeComponent();
            System.Console.WriteLine("~app started!");
            this.ShowInTaskbar = false;
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            this.Top = 0;
            Info informacie = new Info();

            /* treba pozdeji zmenit :) pocasi se nacte az pozdeji po spusteni, pokud to bude narusovat chod programu klidne zakomentovat... */

            if (informacie.pripojen_k_internetu)
            {
                /* Cekej, nez se nacte pocasi */
                while (informacie.teplota == null)
                {
                    ;
                }
            }

            /*
             Casovac
            */
            this.timerCas = new System.Windows.Threading.DispatcherTimer();
            this.timerCas.Tick += new EventHandler(nastavCas);
            this.timerCas.Interval = new TimeSpan(0, 0, 1); //1 sekunda
            this.timerCas.Start();



            /* sekce pocasi */
            if (informacie.pripojen_k_internetu)
            {
                teplota.Content = informacie.teplota + " °C";
                lokace.Content = informacie.lokacia;
                pocasi.Content = informacie.pocasie;
                pocasi_obr.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(informacie.obrazokURL);
            }
            else
            {
                teplota.Content = "";
                lokace.Content = "";
                pocasi.Content = "";

            }
            //System.Console.WriteLine(informacie.obrazokURL);

            /* zde lze nastavit velikost okna, ostatni se automaticky upravi na prijatelnou velikost 
             - pozdeji by bylo fajn (az bude mozne modifikovat velikost okna za behu programu) vlozit nasledici radky do reakce na zmenu velikosti okna
            */
            okno.Width = okno.Height = 400; /* nastav velikost okna 200 az INFINITY  *///default 400
            resize();

            if (informacie.pripojen_k_internetu)
            {
                teplota.Height = okno.Height / 3;
                teplota.FontSize = okno.Width / 20;
                teplota.Width = okno.Width / 5.7;
                lokace.Height = okno.Height / 2.5;
                lokace.FontSize = okno.Height / 16;
                lokace.Width = okno.Width / 3.33;
                pocasi.Height = okno.Width / 2.2;
                pocasi.FontSize = okno.Width / 16;
                pocasi.Width = okno.Width / 3.33;
                pocasi_obr.Width = pocasi_obr.Height = okno.Width / 2;
            }
            //kolecko.Fill = "Red";
            
    }


        public void resize()
        {
            //okno.Width = okno.Height = 400; /* nastav velikost okna 200 az INFINITY  */
            kolecko.Width = kolecko.Height = okno.Width - 50;
            sekundaRucicka.Height = okno.Width / 2;
            sekundaRucicka.Width = okno.Width / 40;
            minutaRucicka.Height = okno.Width / 2;
            minutaRucicka.Width = okno.Width / 30;
            hodinaRucicka.Height = okno.Width / 2;
            hodinaRucicka.Width = okno.Width / 20;
            digitalTime.Height = okno.Width / 2;
            digitalTime.FontSize = okno.Width / 10;
            datum.Height = okno.Width / 4;
            datum.FontSize = okno.Width / 20;
            stred.Width = stred.Height = okno.Width / 10;
        }


        /*
         * Metoda, ktera nastavi analogovy a digitalni cas
         */
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

        /*
         * Ukonci aplikaciu
         */
        private void Ukoncit(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("Ukoncuji aplikace.....");
            Environment.Exit(0);
        }

        /*
            Credits
        */
        private void Credits(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("Credits: bla bla bla");
        }

        /*
            Settings
        */
        private void Settings(object sender, RoutedEventArgs e)
        {
            Settings nastavenia = new Settings();
            nastavenia.Show();
            /* nastaveni pozice podle umisteni hodin */
            if (okno.Left > SystemParameters.PrimaryScreenWidth / 2.0)
            {
                nastavenia.Left = okno.Left - 0.75*okno.Width;
            }
            else
            {
                nastavenia.Left = okno.Left + okno.Width;
            }
            nastavenia.Top = okno.Top;
            System.Console.WriteLine("Settings: blalalall");
            //kolecko.Opacity = nastavenia.opacity.Value / 10.0;

        }



    }
}
