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
        SolidColorBrush BG;//deafault settings background color


        public Settings()
        {
           
            InitializeComponent();
            string[] skiny = Directory.GetDirectories(System.Environment.CurrentDirectory + "\\Skins");

            //initialize bg all color
            BG = new SolidColorBrush(Colors.DimGray);
            settWin.Background = BG;
            settMain.Background = BG;
            settVisual.Background = BG;
            settClock.Background = BG;
            settWeath.Background = BG;

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
            zobraz_pocasi.IsChecked = Properties.Settings.Default.zobraz_pocasi;
            zapnout_tikani.IsChecked = Properties.Settings.Default.tikani;
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

        /* 
         * Metoda reagujici na zmenu hodnoty baru pruhlednosti 
         */
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

        /* 
         * Metoda reagujici na zmenu hodnoty baru velikosti 
         */
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

        /*
         * Metoda pro zapnutí počasí
         */
        private void zobrazPocasiTrue(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("Zobraz pocasi");
            Properties.Settings.Default.zobraz_pocasi = true;
            MainWindow.I.pocasi.Visibility = Visibility.Visible;
            MainWindow.I.lokace.Visibility = Visibility.Visible;
            MainWindow.I.pocasi_obr.Visibility = Visibility.Visible;
            MainWindow.I.teplota.Visibility = Visibility.Visible;
        }

        /*
         * Metoda pro vypnutí počasí
         */
        private void zobrazPocasiFalse(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("Skryj pocasi");
            Properties.Settings.Default.zobraz_pocasi = false;
            MainWindow.I.pocasi.Visibility = Visibility.Hidden;
            MainWindow.I.lokace.Visibility = Visibility.Hidden;
            MainWindow.I.pocasi_obr.Visibility = Visibility.Hidden;
            MainWindow.I.teplota.Visibility = Visibility.Hidden;
        }

        /*
         * Metoda pro zapnutí tikání analogových hodin
         */
        private void tikaniTrue(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("Tikání zapnuto");
            Properties.Settings.Default.tikani = true;
        }

        /*
         * Metoda pro vypnutí tikání analogových hodin
         */
        private void tikaniFalse(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("Tikání vypnuto");
            Properties.Settings.Default.tikani = false;
        }

        /*
         * Metoda menici barvu hodin
         */
        private void nastavBarvuKola(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("barva hodin zmenena");
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom(BarvaKola.SelectedColor.ToString());
            MainWindow.I.kolecko.Fill = brush;
        }
        
        /*
         * Metoda menici barvu hodinove rucicky
         */
        private void nastavBarvuH(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("barva hodinove rucicky zmenena");
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom(BarvaHRaf.SelectedColor.ToString());
            MainWindow.I.hodinaRucicka.Fill = brush;
        }
        
        /*
         * Metoda menici barvu minutove rucicky
         */
        private void nastavBarvuM(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("barva minutove rucicky zmenena");
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom(BarvaMRaf.SelectedColor.ToString());
            MainWindow.I.minutaRucicka.Fill = brush;
        }

        /*
         * Metoda menici barvu sekundove rucicky
         */
        private void nastavBarvuS(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("barva sekundove rucicky zmenena");
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom(BarvaSRaf.SelectedColor.ToString());
            MainWindow.I.sekundaRucicka.Fill = brush;
        }
        
        /*
         * Metoda menici barvu stredu hodin
         */
        private void nastavBarvuStredu(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("barva stredu zmenena");
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom(BarvaStred.SelectedColor.ToString());
            MainWindow.I.stred.Fill = brush;
        }

        /*
         * Metoda menici barvu ciferniku analogovych hodin
         */
        private void nastavBarvuC(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("barva stredu zmenena");
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom(BarvaCifernik.SelectedColor.ToString());
            MainWindow.I.c1.Foreground = brush;
            MainWindow.I.c2.Foreground = brush;
            MainWindow.I.c3.Foreground = brush;
            MainWindow.I.c4.Foreground = brush;
            MainWindow.I.c5.Foreground = brush;
            MainWindow.I.c6.Foreground = brush;
            MainWindow.I.c7.Foreground = brush;
            MainWindow.I.c8.Foreground = brush;
            MainWindow.I.c9.Foreground = brush;
            MainWindow.I.c10.Foreground = brush;
            MainWindow.I.c11.Foreground = brush;
            MainWindow.I.c12.Foreground = brush;
        }


        private void chmode(object sender, RoutedEventArgs e)
        {
            Button tmp = sender as Button;
            
            MainWindow.I.change_mode(Int32.Parse(tmp.ToolTip.ToString()));
        }





        private void toVisual(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("VISALCLK");
            settUnvisible.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            settVisualLoad.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void toOld(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("OLDCKLL");
            settUnvisible.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            settOld.Visibility = Visibility.Visible;
            settOldLoad.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
    }
}
