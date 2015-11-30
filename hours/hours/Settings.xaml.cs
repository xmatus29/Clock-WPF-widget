using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Settings : Window
    {

        

        public Settings()
        {
           
            InitializeComponent();
            string[] skiny = Directory.GetDirectories(System.Environment.CurrentDirectory + "\\Skins");

            foreach (string skin in skiny)
            {
                SkinBox.Items.Add(System.IO.Path.GetFileName(skin));
                //nahlad.Source = new BitmapImage(new Uri(skin + "\\thumb.png"));
            }
            start();
            setInfobox();
        }


        public void start()
        {
            opacity.Value = Properties.Settings.Default.all_opacity;
            size.Value = Properties.Settings.Default.all_size; 
            all_top.IsChecked = Properties.Settings.Default.all_top;
        }

        public void setInfobox()
        {
            infobox.Text = "Runs count:" + Properties.Settings.Default.runs.ToString();
        }



        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            MainWindow.I.nastavenia = null;
        }








        private void vyber(object sender, SelectionChangedEventArgs e)
        {
            string skin = (sender as ComboBox).SelectedItem as string;
            nahlad.Source = new BitmapImage(new Uri(System.Environment.CurrentDirectory + "\\Skins\\" + skin + "\\thumb.png"));
        }

        /* metoda reagujici na zmenu hodnoty baru pruhlednosti */
        private void change_opacity(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("opacity change on: " + opacity.Value/10.0 );
            Properties.Settings.Default.all_opacity = opacity.Value; // save opacity slider val
            if (opacity.Value < 1.2)
            {
                return;
            }

            MainWindow.I.Opacity = opacity.Value / 10.0;
            
        }

        /* metoda reagujici na zmenu hodnoty baru velikosti */
        private void change_size(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("size change on: " + size.Value / 10.0);
            Properties.Settings.Default.all_size = size.Value; // save size val
            if (size.Value < 2.0)
            {
                return;
            }

            
           

            MainWindow.I.Height = (size.Value * 100);
            MainWindow.I.Width = (size.Value * 100);
            MainWindow.I.resize();

        }

        /*
         * Nastavene aby bolo okno vzdy narvchu
         */
        private void vzdyNavrchuTrue(object sender, RoutedEventArgs e)
        {
            MainWindow.I.Topmost = true;
            Properties.Settings.Default.all_top = true;
        }

        /*
         * Nastavene ze neni okno vzdy navrhu
         */
        private void vzdyNavrchuFalse(object sender, RoutedEventArgs e)
        {
            MainWindow.I.Topmost = false;
            Properties.Settings.Default.all_top = false;
        }

        private void zobrazPocasiTrue(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("Zobraz pocasi");
            MainWindow.I.pocasi.Visibility = Visibility.Visible;
            MainWindow.I.lokace.Visibility = Visibility.Visible;
            MainWindow.I.pocasi_obr.Visibility = Visibility.Visible;
            MainWindow.I.teplota.Visibility = Visibility.Visible;
            Properties.Settings.Default.zobraz_pocasi = true;
        }

        private void zobrazPocasiFalse(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("Skryj pocasi");
            MainWindow.I.pocasi.Visibility = Visibility.Hidden;
            MainWindow.I.lokace.Visibility = Visibility.Hidden;
            MainWindow.I.pocasi_obr.Visibility = Visibility.Hidden;
            MainWindow.I.teplota.Visibility = Visibility.Hidden;
            Properties.Settings.Default.zobraz_pocasi = false;
        }

        private void chmode(object sender, RoutedEventArgs e)
        {
            Button tmp = sender as Button;
            
            MainWindow.I.change_mode(Int32.Parse(tmp.ToolTip.ToString()));
        }
    }
}
