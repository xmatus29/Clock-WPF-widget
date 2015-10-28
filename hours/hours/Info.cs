using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;



namespace hours
{
    /*
     * Trieda na zistenie pocasia z internetu + casu
     * @Author: Filip Gulan
     */
    class Info
    {
        public String teplota;
        public String lokacia;
        public String epochCas;
        public String pocasie;
        public String rychlostVetra;
        public String obrazokURL;
        public String tlak;
        public String viditelnost;
        public String vlhkost;
        public String cas;
        public String hodina;
        public String sekunda;
        public String minuta;

        private System.Windows.Threading.DispatcherTimer timerPocasie;
        private System.Windows.Threading.DispatcherTimer timerCas;

        /*
         * Konstruktor triedy
         */
        public Info()
        {
            //Nastavenie Casovacu pre ziskavanie casu kazdu sekundu
            this.timerCas = new System.Windows.Threading.DispatcherTimer();
            this.timerCas.Tick += new EventHandler(ziskajCas);
            this.timerCas.Interval = new TimeSpan(0, 0, 1); //1 sekunda
            this.timerCas.Start();

            //Nastavenie casovacu pre ziskavanie pocasia kazdu hodinu
            this.timerPocasie = new System.Windows.Threading.DispatcherTimer();
            this.timerPocasie.Tick += new EventHandler(ziskajPocasieTimer);
            this.timerPocasie.Interval = new TimeSpan(1, 0, 0); //1 hodina
            this.timerPocasie.Start();

            Thread thread = new Thread(ziskajPocasie); //prve spustenie na zaciatku ziskanie pocasie asynchrone
            thread.Start();
        }

        /*
         * Metoda na periodicke spustanie funkcie na ziaskanie pocasia
         */
        private void ziskajPocasieTimer(object sender, EventArgs e)
        {
            ziskajPocasie();
        }

        /*
         * Metoda na ziskanie pocasia z internetu
         */
        private void ziskajPocasie()
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load("http://api.wunderground.com/api/3682ad28155eee3a/conditions/lang:en/q/autoip.xml");
            }
            catch
            {
                Console.WriteLine("Problem s pripojenim!");
            }
            this.lokacia = doc.SelectSingleNode("//city").InnerText;
            this.teplota = doc.SelectSingleNode("//temp_c").InnerText;
            this.epochCas = doc.SelectSingleNode("//local_epoch").InnerText;
            this.pocasie = doc.SelectSingleNode("//weather").InnerText;
            this.rychlostVetra = doc.SelectSingleNode("//wind_mph").InnerText;
            this.obrazokURL = doc.SelectSingleNode("//icon_url").InnerText;
            this.tlak = doc.SelectSingleNode("//pressure_mb").InnerText;
            this.viditelnost = doc.SelectSingleNode("//visibility_km").InnerText;
            this.vlhkost = doc.SelectSingleNode("//relative_humidity").InnerText;
            //Console.WriteLine(this.vlhkost);
        }

        /*
         * Metoda na periodicke ziskavanie casu
         */
        private void ziskajCas(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            this.cas = time.ToString("hh:mm:ss");
            this.hodina = time.ToString("hh");
            this.minuta = time.ToString("mm");
            this.sekunda = time.ToString("ss");
            //Console.WriteLine(this.sekunda);
        }
    }
}
