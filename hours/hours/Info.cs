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

        private System.Windows.Threading.DispatcherTimer timerPocasie;

        /*
         * Konstruktor triedy
         */
        public Info()
        {
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
            {
                if (this.lokacia != null) this.lokacia = doc.SelectSingleNode("//city").InnerText;
                if (this.teplota != null) this.teplota = doc.SelectSingleNode("//temp_c").InnerText;
                if (this.epochCas != null) this.epochCas = doc.SelectSingleNode("//local_epoch").InnerText;
                if (this.pocasie != null) this.pocasie = doc.SelectSingleNode("//weather").InnerText;
                if (this.rychlostVetra != null) this.rychlostVetra = doc.SelectSingleNode("//wind_mph").InnerText;
                if (this.obrazokURL != null) this.obrazokURL = doc.SelectSingleNode("//icon_url").InnerText;
                if (this.tlak != null) this.tlak = doc.SelectSingleNode("//pressure_mb").InnerText;
                if (this.viditelnost != null) this.viditelnost = doc.SelectSingleNode("//visibility_km").InnerText;
                if (this.vlhkost != null) this.vlhkost = doc.SelectSingleNode("//relative_humidity").InnerText;
                //Console.WriteLine(this.vlhkost);
            }
         }
    }
}
