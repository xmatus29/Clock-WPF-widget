using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Net;



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
        public bool pripojen_k_internetu;
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
            pripojen_k_internetu = CheckConnection("https://www.google.cz"); //overeni pripojeni k internetu

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
            if (pripojen_k_internetu)
            {
                this.lokacia = doc.SelectSingleNode("//city").InnerText;
                this.teplota = doc.SelectSingleNode("//temp_c").InnerText;
                this.epochCas = doc.SelectSingleNode("//local_epoch").InnerText;
                this.pocasie = doc.SelectSingleNode("//weather").InnerText;
                this.rychlostVetra = doc.SelectSingleNode("//wind_mph").InnerText;
                this.obrazokURL = doc.SelectSingleNode("//icon_url").InnerText;
                this.tlak = doc.SelectSingleNode("//pressure_mb").InnerText;
                this.viditelnost = doc.SelectSingleNode("//visibility_km").InnerText;
                this.vlhkost = doc.SelectSingleNode("//relative_humidity").InnerText;
            }
                //Console.WriteLine(this.teplota);
         }

        /*
            Overeni pripojeni k internetu
        */
        private bool CheckConnection(String URL)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Timeout = 5000;
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK) return true;
                else return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
