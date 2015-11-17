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
        
        public Settings nastavenia = null;
        public Credits credits = null;
        public Info informacie = null;





        public MainWindow()
        {

            I = this;
            
            InitializeComponent();

           

            







            
            this.ShowInTaskbar = false;
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
            this.Top = 0;
            informacie = new Info();

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


            //DEFAULT FIRST RUN
            if (Properties.Settings.Default.runs == 0)
            {
                Properties.Settings.Default.all_opacity = Opacity*10;
                Properties.Settings.Default.all_size = Width/100;
                Properties.Settings.Default.all_wleft =  Double.Parse( SystemParameters.PrimaryScreenWidth.ToString()) - Double.Parse(Width.ToString());
                Properties.Settings.Default.all_wtop = 0.0;
                Properties.Settings.Default.all_top = true;
            }
            Properties.Settings.Default.runs += 1;

            Opacity = Properties.Settings.Default.all_opacity / 10;
            Width = Height = Properties.Settings.Default.all_size * 100;
            MainWindow.I.resize();

            //position on screen
            Left = Properties.Settings.Default.all_wleft;
            Top = Properties.Settings.Default.all_wtop;





            //RUN
            change_mode(Properties.Settings.Default.mode);
            System.Console.WriteLine("~app started!:" + Properties.Settings.Default.runs);
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
                //position
                Properties.Settings.Default.all_wleft = MainWindow.I.Left;
                Properties.Settings.Default.all_wtop = MainWindow.I.Top;
            }
            
        }

        /*
         * Metoda, tkora sa stara aby bolo okno stale navrchu vsetkeho
         */
        private void staleNavrchu(object sender, EventArgs e)
        {
            if(Properties.Settings.Default.all_top == true)
            {
                Window window = (Window)sender;
                window.Topmost = true;
            }
        }

        /*
         * Ukonci aplikaciu
         */
        private void Ukoncit(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("Ukoncuji aplikace.....");
            Properties.Settings.Default.Save();
            Environment.Exit(0);
        }

        /*
            Credits
        */
        private void Credits(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("Credits: Start");
            if (this.credits != null) { return; }
            this.credits = new Credits();
            credits.Show();
            if (okno.Left > SystemParameters.PrimaryScreenWidth / 2.0)
            {
                credits.Left = okno.Left - 0.5 * okno.Width;
            }
            else
            {
                credits.Left = okno.Left + okno.Width;
            }
            credits.Top = okno.Top;
        }

        /*
            Settings
        */
        private void Settings(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("Settings: Start");
            if (this.nastavenia != null){return;}
            this.nastavenia = new Settings();
            this.nastavenia.Show();

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

        }




        public void change_mode(int x)
        {
            Properties.Settings.Default.mode = x;
            MainWindow.I.DIGITAL.Visibility = Visibility.Hidden;
            MainWindow.I.BINARY.Visibility = Visibility.Hidden;
            MainWindow.I.ANALOG.Visibility = Visibility.Hidden;

            switch (x)
            {
                case 0://analog
                    MainWindow.I.ANALOG.Visibility = Visibility.Visible;
                    break;
                case 1://digital
                    MainWindow.I.DIGITAL.Visibility = Visibility.Visible;
                    break;
                case 2://binary
                    MainWindow.I.BINARY.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }












        //BINARY
        public static void Serialize<T>(T t)
        {
            //using (System.IO.StringWriter sw = new System.IO.StringWriter())
            //using (System.Xml.XmlWriter xw = System.Xml.XmlWriter.Create(sw))
            //{
            //    new System.Xml.Serialization.XmlSerializer(typeof(T)).Serialize(xw, t);
            //    return sw.GetStringBuilder().ToString();
           // }

            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (var writer = new System.IO.StreamWriter(@"testxxx.xml"))
            {
                serializer.Serialize(writer, t);
            }

            
        }

        private void BINARY_CLICK(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("RESET");
            Properties.Settings.Default.Reset();
            //Serialize<Info>(this.informacie);
        }
    }
}
