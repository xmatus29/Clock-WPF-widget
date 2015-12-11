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
        public Alarm alarm = null;
        public AutoTurn auto = null;
        public Stopwatch stopky = null;
        public Info informacie = null;
        public bool tikani;
        BitmapImage binarnaNula;
        BitmapImage binarnaJedna;

        Alarm aa = new Alarm();
        MediaPlayer mplayer = new MediaPlayer();



        public MainWindow()
        {



            I = this;
            InitializeComponent();
            Properties.Settings.Default.alarmMusic = (System.Environment.CurrentDirectory + "\\Sounds\\" + "\\Loud-alarm-clock-sound.wav");

            if (Properties.Settings.Default.zobraz_pocasi == true)
            {

                MainWindow.I.pocasi.Visibility = Visibility.Visible;
                MainWindow.I.lokace.Visibility = Visibility.Visible;
                MainWindow.I.pocasi_obr.Visibility = Visibility.Visible;
                MainWindow.I.teplota.Visibility = Visibility.Visible;
            }
            else
            {

                MainWindow.I.pocasi.Visibility = Visibility.Hidden;
                MainWindow.I.lokace.Visibility = Visibility.Hidden;
                MainWindow.I.pocasi_obr.Visibility = Visibility.Hidden;
                MainWindow.I.teplota.Visibility = Visibility.Hidden;
            }

            this.binarnaJedna = new BitmapImage(new Uri(System.Environment.CurrentDirectory + "\\Skins\\thumb.png"));
            this.binarnaNula = new BitmapImage(new Uri(System.Environment.CurrentDirectory + "\\Skins\\thumb1.png"));

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
                //pocasi_obr.Source = (ImageSource)new ImageSourceConverter().ConvertFromString(informacie.obrazokURL);
            }
            else
            {
                teplota.Content = "";
                lokace.Content = "";
                pocasi.Content = "";

            }

            resize();

            //DEFAULT FIRST RUN
            if (Properties.Settings.Default.runs == 0)
            {
                Properties.Settings.Default.all_opacity = Opacity*10;
                Properties.Settings.Default.all_size = Width/100;
                Properties.Settings.Default.all_wleft =  Double.Parse( SystemParameters.PrimaryScreenWidth.ToString()) - Double.Parse(Width.ToString());
                Properties.Settings.Default.all_wtop = 0.0;
                Properties.Settings.Default.all_top = true;
                Properties.Settings.Default.zobraz_pocasi = true;
                Properties.Settings.Default.tikani = false;
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

            //OPEN SETTINGS ON STARTUP
            //RUNSETT.RaiseEvent(new RoutedEventArgs(MenuItem.ClickEvent));
        }

        /*
         * Metoda, ktera dynamicky meni vsechny casti hodin
         */
        public void resize()
        {
            /*
                Hodiny
            */
            kolecko.Width = kolecko.Height = okno.Width / 1.6;
            sekundaRucicka.Height = okno.Width / 2.66;
            sekundaRucicka.Width = okno.Width / 40;
            minutaRucicka.Height = okno.Width / 2.66;
            minutaRucicka.Width = okno.Width / 30;
            hodinaRucicka.Height = okno.Width / 2.66;
            hodinaRucicka.Width = okno.Width / 20;
            digitalTime.FontSize = okno.Width / 10;
            datum.Height = okno.Width / 4;
            datum.FontSize = okno.Width / 20;
            stred.Width = stred.Height = okno.Width / 10;

            /*
                Cifernik
            */
            c1.Height = okno.Height/1.57;
            c1.Width = okno.Width/2.67;
            c1.FontSize = okno.Width/13.3;

            c2.Height = okno.Height / 1.905;
            c2.Width = okno.Width / 3.64;
            c2.FontSize = okno.Width / 13.3;

            c3.Height = okno.Height / 2.5;
            c3.Width = okno.Width / 4;
            c3.FontSize = okno.Width / 13.3;

            c4.Height = okno.Height / 3.63;
            c4.Width = okno.Width / 3.63;
            c4.FontSize = okno.Width / 13.3;

            c5.Height = okno.Height / 5.17;
            c5.Width = okno.Width / 2.67;
            c5.FontSize = okno.Width / 13.3;

            c6.Height = okno.Height / 8;
            c6.FontSize = okno.Width / 13.3;

            c7.Height = okno.Height / 5.71;
            c7.Width = okno.Width / 1.53;
            c7.FontSize = okno.Width / 13.3;

            c8.Height = okno.Height / 3.63;
            c8.Width = okno.Width / 1.33;
            c8.FontSize = okno.Width / 13.3;

            c9.Height = okno.Height / 2.5;
            c9.Width = okno.Width / 1.25;
            c9.FontSize = okno.Width / 13.3;

            c10.Height = okno.Height / 1.9;
            c10.Width = okno.Width / 1.28;
            c10.FontSize = okno.Width / 13.3;

            c11.Height = okno.Height / 1.54;
            c11.Width = okno.Width / 1.48;
            c11.FontSize = okno.Width / 13.3;

            c12.Height = okno.Height / 1.45;
            c12.FontSize = okno.Width / 13.3;


            if (informacie.pripojen_k_internetu)
            {
                teplota.Height = okno.Height / 10;
                teplota.FontSize = okno.Width / 20;
                lokace.Height = okno.Height / 10;
                lokace.FontSize = okno.Height / 20;
                pocasi.Height = okno.Width / 10;
                pocasi.FontSize = okno.Width / 20;
                pocasi_obr.Width = pocasi_obr.Height = okno.Width /10;
            }
        }


        /*
         * Metoda, ktera nastavi analogovy a digitalni cas
         */
        private void nastavCas(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.mode == 0)
            {
                if (Properties.Settings.Default.tikani)
                {
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(System.Environment.CurrentDirectory + "\\Sounds\\" + "\\clock-ticking-2_1s.wav");
                    player.Play();
                }
                sekunda.Angle = DateTime.Now.Second * 6;
                minuta.Angle = DateTime.Now.Minute * 6;
                hodina.Angle = DateTime.Now.Minute * 0.5 + DateTime.Now.Hour * 30;
                datum.Content = DateTime.Now.DayOfWeek + "  " + DateTime.Now.Day + ". " + DateTime.Now.Month + ". " + DateTime.Now.Year;
            }

            if (Properties.Settings.Default.mode == 1)
            {
                digitalTime.Content = DateTime.Now.Hour.ToString() + "h " + DateTime.Now.Minute.ToString() + "m " + DateTime.Now.Second.ToString() + "s";
                datum.Content = DateTime.Now.DayOfWeek + "  " + DateTime.Now.Day + ". " + DateTime.Now.Month + ". " + DateTime.Now.Year;
            }

            if (Properties.Settings.Default.mode == 2)
            {
                BINARY.Children.Clear();
                //Binarne sekudny
                this.vykresliBinarneHodiny(new Image[6], Convert.ToString(DateTime.Now.Second, 2), 2);
                //Binarne minuty
                this.vykresliBinarneHodiny(new Image[6], Convert.ToString(DateTime.Now.Minute, 2), 1);
                //Binarne hodiny
                this.vykresliBinarneHodiny(new Image[6], Convert.ToString(DateTime.Now.Hour, 2), 0);
            }

            if (Properties.Settings.Default.alarm == true && DateTime.Now.Hour.ToString() == Properties.Settings.Default.alarmHodiny && DateTime.Now.Minute.ToString() == Properties.Settings.Default.alarmMinuty && DateTime.Now.Second.ToString() == "0")
            {
                this.mplayer.Open(new Uri(Properties.Settings.Default.alarmMusic, UriKind.Relative));
                this.mplayer.Play();
            }

            if (Properties.Settings.Default.auto == true && DateTime.Now.Hour.ToString() == Properties.Settings.Default.autoHodiny && DateTime.Now.Minute.ToString() == Properties.Settings.Default.autoMinuty && DateTime.Now.Second.ToString() == "0")
            {
                System.Diagnostics.Process.Start("shutdown", "/s /t 30");
            }
           
        }

        /*
         * Metoda ktora vykresluje binarne hodiny
         */
        private void vykresliBinarneHodiny(Image[] poleBitov, string binarne, int riadok)
        {
            //Umiestnujem obrazky podla hodnoty bitov
            for (int i = 0; i < binarne.Length; i++)
            {
                //Doplni nuly na zaciatok, aby sa vykrelsovali aj nuly vzdy 6 cisiel
                for (int j = 0; j < 6 - binarne.Length; j++)
                {
                    binarne = binarne.Insert(0, "0");
                }

                poleBitov[i] = new Image();
                if (binarne[i] == '1') //je tam jendotka tak zasvietim
                {
                    poleBitov[i] = new Image();
                    poleBitov[i].Source = this.binarnaJedna;
                }
                else //inak vypnem
                {
                    poleBitov[i].Source = this.binarnaNula;
                }
                BINARY.Children.Add(poleBitov[i]);
                Grid.SetColumn(poleBitov[i], i);
                Grid.SetRow(poleBitov[i], riadok);
            }
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
            this.mplayer.Stop();           
        }

        /*
         * Metoda, ktora sa stara aby bolo okno stale navrchu vsetkeho
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
         * Credits
         */
        private void Credits(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("Credits: Start");
            if (this.credits != null) { return; }
            this.credits = new Credits();
            credits.Show();
            
            if (okno.Left > SystemParameters.PrimaryScreenWidth / 2.0)
            {
                credits.Left = okno.Left - credits.Width - 20;
            }
            else
            {
                credits.Left = okno.Left + okno.Width + 20;
            }
            credits.Top = okno.Top;
        }

        private void Alarm(object sender, RoutedEventArgs e)
        {
            this.alarm = new Alarm();
            this.alarm.Show();

            /* nastaveni pozice podle umisteni hodin */
            if (okno.Left > SystemParameters.PrimaryScreenWidth / 2.0)
            {
                alarm.Left = okno.Left - alarm.Width - 20;
            }
            else
            {
                alarm.Left = okno.Left + okno.Width + 20;
            }
            if (okno.Top < SystemParameters.PrimaryScreenHeight / 2.0)
            {
                alarm.Top = okno.Top;
            }
            else
            {
                alarm.Top = SystemParameters.PrimaryScreenHeight - alarm.Height - 50;
            }
        }

        private void AutoTurnOff(object sender, RoutedEventArgs e)
        {
            this.auto = new AutoTurn();
            this.auto.Show();
        }

        private void StopWatch(object sender, RoutedEventArgs e)
        {
            this.stopky = new Stopwatch();
            this.stopky.Show();
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

            /* nastaveni pozice settings podle umisteni hodin */
            if (okno.Left > SystemParameters.PrimaryScreenWidth / 2.0)
            {
                nastavenia.Left = okno.Left - nastavenia.Width-20;
            }
            else
            {
                nastavenia.Left = okno.Left + okno.Width + 20;
            }
            if (okno.Top < SystemParameters.PrimaryScreenHeight / 2.0)
            {
                nastavenia.Top = okno.Top;
            }
            else
            {
                nastavenia.Top = SystemParameters.PrimaryScreenHeight - nastavenia.Height - 50;
            }
        }

        /*
         * Volba modu hodin
         */
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

        /*
         * Binarni hodiny
         */
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
            //Properties.Settings.Default.Reset();
            //Serialize<Info>(this.informacie);
        }
    }
}
