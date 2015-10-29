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
            Info informacie = new Info();

            /*
             Pouzito z INFO ...  nedarilo se mi k te tride dostat ...
            */
            this.timerCas = new System.Windows.Threading.DispatcherTimer();
            this.timerCas.Tick += new EventHandler(nastavCas);
            this.timerCas.Interval = new TimeSpan(0, 0, 1); //1 sekunda
            this.timerCas.Start();
            

        }

        private void nastavCas(object sender, EventArgs e)
        {
            sekunda.Angle = DateTime.Now.Second * 6;
            minuta.Angle = DateTime.Now.Minute * 6;
            hodina.Angle = DateTime.Now.Minute * 0.5 + DateTime.Now.Hour * 30;
            //Console.WriteLine(this.sekunda);
        }
    }
}
