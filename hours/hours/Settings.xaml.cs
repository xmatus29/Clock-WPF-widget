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
            settOld.Background = BG;

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

            w_city.IsChecked = Properties.Settings.Default.w_city;
            w_temp.IsChecked = Properties.Settings.Default.w_temp;
            w_info.IsChecked = Properties.Settings.Default.w_info;
            w_obr.IsChecked = Properties.Settings.Default.w_obr;
            w_datum.IsChecked = Properties.Settings.Default.w_datum;

            //clock checkers
            c0_msec.IsChecked = Properties.Settings.Default.c0_msec;
            c0_sec.IsChecked = Properties.Settings.Default.c0_sec;
            c0_min.IsChecked = Properties.Settings.Default.c0_min;
            c0_hour.IsChecked = Properties.Settings.Default.c0_hour;
            c0_mid.IsChecked = Properties.Settings.Default.c0_mid;
            c0_cif.IsChecked = Properties.Settings.Default.c0_cif;
            c0_pod.IsChecked = Properties.Settings.Default.c0_pod;

            MainWindow.I.w_reload();
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
            MainWindow.I.w_all.Visibility = Visibility.Visible;
            MainWindow.I.w_reload();
        }

        /*
         * Metoda pro vypnutí počasí
         */
        private void zobrazPocasiFalse(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("Skryj pocasi");
            Properties.Settings.Default.zobraz_pocasi = false;
            MainWindow.I.w_all.Visibility = Visibility.Hidden;
            MainWindow.I.w_reload();
        }


        /*
         * Metoda CITY ON
         */
        private void w_city_true(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.w_city = true;
            MainWindow.I.w_city_grid.Visibility = Visibility.Visible;
            MainWindow.I.w_reload();
        }

        /*
         * Metoda CITY OOFF
         */
        private void w_city_false(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.w_city = false;
            MainWindow.I.w_city_grid.Visibility = Visibility.Hidden;
            MainWindow.I.w_reload();
        }


        /*
         * Metoda INFO ON
         */
        private void w_info_true(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.w_info = true;
            MainWindow.I.w_info_grid.Visibility = Visibility.Visible;
            MainWindow.I.w_reload();
        }

        /*
         * Metoda INFO OOFF
         */
        private void w_info_false(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.w_info = false;
            MainWindow.I.w_info_grid.Visibility = Visibility.Hidden;
            MainWindow.I.w_reload();
        }


        /*
         * Metoda TEMP ON
         */
        private void w_temp_true(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.w_temp = true;
            MainWindow.I.w_temp_grid.Visibility = Visibility.Visible;
            MainWindow.I.w_reload();
        }

        /*
         * Metoda TEMP OOFF
         */
        private void w_temp_false(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.w_temp = false;
            MainWindow.I.w_temp_grid.Visibility = Visibility.Hidden;
            MainWindow.I.w_reload();
        }

        /*
         * Metoda OBR ON
         */
        private void w_obr_true(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.w_obr = true;
            MainWindow.I.w_obr_grid.Visibility = Visibility.Visible;
            MainWindow.I.w_reload();
        }

        /*
         * Metoda OBR OOFF
         */
        private void w_obr_false(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.w_obr = false;
            MainWindow.I.w_obr_grid.Visibility = Visibility.Hidden;
            MainWindow.I.w_reload();
        }

        /*
         * Metoda DATUM ON
         */
        private void w_datum_true(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.w_datum = true;
            MainWindow.I.w_datum_grid.Visibility = Visibility.Visible;
            MainWindow.I.w_reload();
        }

        /*
         * Metoda DATUM OFF
         */
        private void w_datum_false(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.w_datum = false;
            MainWindow.I.w_datum_grid.Visibility = Visibility.Hidden;
            MainWindow.I.w_reload();
        }














        //checkboxs CLOCK

        /*
        * Metoda CLOCK0_MSEC ON
        */
        private void c0_msec_true(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-ANALOG-> msec on");
            Properties.Settings.Default.c0_msec = true;
            MainWindow.I.sekundaRucickaO.Visibility = Visibility.Visible;
        }
        /*
         * Metoda CLOCK0_MSEC OFF
         */
        private void c0_msec_false(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-ANALOG-> msec off");
            Properties.Settings.Default.c0_msec = false;
            MainWindow.I.sekundaRucickaO.Visibility = Visibility.Hidden;
        }


        /*
        * Metoda CLOCK0_SEC ON
        */
        private void c0_sec_true(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-ANALOG-> sec on");
            Properties.Settings.Default.c0_sec = true;
            MainWindow.I.sekundaRucickaO.Visibility = Visibility.Visible;
        }
        /*
         * Metoda CLOCK0_SEC OFF
         */
        private void c0_sec_false(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-ANALOG-> sec off");
            Properties.Settings.Default.c0_sec = false;
            MainWindow.I.sekundaRucickaO.Visibility = Visibility.Hidden;
        }

        /*
        * Metoda CLOCK0_MIN ON
        */
        private void c0_min_true(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-ANALOG-> min on");
            Properties.Settings.Default.c0_min = true;
            MainWindow.I.minutaRucickaO.Visibility = Visibility.Visible;
        }
        /*
         * Metoda CLOCK0_MIN OFF
         */
        private void c0_min_false(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-ANALOG-> min off");
            Properties.Settings.Default.c0_min = false;
            MainWindow.I.minutaRucickaO.Visibility = Visibility.Hidden;
        }


        /*
        * Metoda CLOCK0_HOUR ON
        */
        private void c0_hour_true(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-ANALOG-> hour on");
            Properties.Settings.Default.c0_hour = true;
            MainWindow.I.hodinaRucickaO.Visibility = Visibility.Visible;
        }
        /*
         * Metoda CLOCK0_HOUR OFF
         */
        private void c0_hour_false(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-ANALOG-> hour off");
            Properties.Settings.Default.c0_hour = false;
            MainWindow.I.hodinaRucickaO.Visibility = Visibility.Hidden;
        }


        
        /*
         * Metoda CLOCK0_ MIDDLE ON
         */
        private void c0_mid_true(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-ANALOG-> mid on");
            Properties.Settings.Default.c0_mid = true;
            MainWindow.I.stredO.Visibility = Visibility.Visible;
        }
        /*
         * Metoda CLOCK0_ MIDDLE OFF
         */
        private void c0_mid_false(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-ANALOG-> mid off");
            Properties.Settings.Default.c0_mid = false;
            MainWindow.I.stredO.Visibility = Visibility.Hidden;
        }



        /*
         * Metoda CLOCK0_ CIF ON
         */
        private void c0_cif_true(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-ANALOG-> cif on");
            Properties.Settings.Default.c0_cif = true;
            MainWindow.I.koleckoCifO.Visibility = Visibility.Visible;
        }
        /*
         * Metoda CLOCK0_ CIF OFF
         */
        private void c0_cif_false(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-ANALOG-> cif off");
            Properties.Settings.Default.c0_cif = false;
            MainWindow.I.koleckoCifO.Visibility = Visibility.Hidden;
        }


        /*
         * Metoda CLOCK0_ PODKLAD ON
         */
        private void c0_pod_true(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-ANALOG-> cif on");
            Properties.Settings.Default.c0_pod = true;
            MainWindow.I.koleckoO.Visibility = Visibility.Visible;
        }
        /*
         * Metoda CLOCK0_ PODKLAD OFF
         */
        private void c0_pod_false(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-ANALOG-> cif off");
            Properties.Settings.Default.c0_pod = false;
            MainWindow.I.koleckoO.Visibility = Visibility.Hidden;
        }





        //NOT DEFINED
        /*
        * Metoda CLOCK1_MSEC ON
        */
        private void c1_msec_true(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-DIGITAL-> msec on");
            Properties.Settings.Default.c1_msec = true;
            MainWindow.I.sekundaRucicka.Visibility = Visibility.Visible;
        }
        /*
         * Metoda CLOCK0_MSEC OFF
         */
        private void c1_msec_false(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-DIGITAL-> msec off");
            Properties.Settings.Default.c1_msec = false;
            MainWindow.I.sekundaRucicka.Visibility = Visibility.Hidden;
        }

        /*
        * Metoda CLOCK1_SEC ON
        */
        private void c1_sec_true(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-DIGITAL-> sec on");
            Properties.Settings.Default.c1_sec = true;
            MainWindow.I.sekundaRucicka.Visibility = Visibility.Visible;
        }
        /*
         * Metoda CLOCK0_SEC OFF
         */
        private void c1_sec_false(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-DIGITAL-> sec off");
            Properties.Settings.Default.c1_sec = false;
            MainWindow.I.sekundaRucicka.Visibility = Visibility.Hidden;
        }

        /*
        * Metoda CLOCK1_MIN ON
        */
        private void c1_min_true(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-DIGITAL-> min on");
            Properties.Settings.Default.c1_min = true;
            MainWindow.I.sekundaRucicka.Visibility = Visibility.Visible;
        }
        /*
         * Metoda CLOCK0_MIN OFF
         */
        private void c1_min_false(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-DIGITAL-> min off");
            Properties.Settings.Default.c1_min = false;
            MainWindow.I.sekundaRucicka.Visibility = Visibility.Hidden;
        }

        /*
        * Metoda CLOCK1_HOUR ON
        */
        private void c1_hour_true(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-DIGITAL-> hour on");
            Properties.Settings.Default.c1_hour = true;
            MainWindow.I.sekundaRucicka.Visibility = Visibility.Visible;
        }
        /*
         * Metoda CLOCK0_HOUR OFF
         */
        private void c1_hour_false(object sender, RoutedEventArgs e)
        {
            MainWindow.I.dbg("CLK-DIGITAL-> hour off");
            Properties.Settings.Default.c1_hour = false;
            MainWindow.I.sekundaRucicka.Visibility = Visibility.Hidden;
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
            Viewbox tmp = sender as Viewbox;
            
            MainWindow.I.change_mode(Int32.Parse(tmp.ToolTip.ToString()));
        }


        private void toDef()
        {
            settUnvisible.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            settMain.Visibility = Visibility.Hidden;
            settVisual.Visibility = Visibility.Hidden;
            settClock.Visibility = Visibility.Hidden;
            settWeath.Visibility = Visibility.Hidden;
            settOld.Visibility = Visibility.Hidden;
        }



        private void toMain(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("MAIN");

            toDef();
            settMainLoad.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            settMain.Visibility = Visibility.Visible;

        }

        private void toVisual(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("VISUAL");
            settUnvisible.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            settVisualLoad.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            settVisual.Visibility = Visibility.Visible;
        }


        private void toClock(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("CLC");
            connClock0.Visibility = Visibility.Hidden;
            connClock1.Visibility = Visibility.Hidden;
            connClock2.Visibility = Visibility.Hidden;

            Label tmp = sender as Label;

            switch (Properties.Settings.Default.mode)
            {
                case 0://analog
                    System.Console.WriteLine("CLC-ANALLOOG");
                    c_text.Text = "Clock - Analog";
                    connClock0.Visibility = Visibility.Visible;

                    break;
                case 1://digital
                    System.Console.WriteLine("CLC-DIGITAL");
                    c_text.Text = "Clock - Digital";
                    connClock1.Visibility = Visibility.Visible;
                    break;
                case 2://binary
                    System.Console.WriteLine("CLC-BINARY");
                    c_text.Text = "Clock - Binary";
                    connClock2.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }




            //make visible
            settUnvisible.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            settClock.Visibility = Visibility.Visible;
            settClockLoad.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }


        private void toWeath(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("WEA");
            settUnvisible.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            settWeath.Visibility = Visibility.Visible;
            settWeathLoad.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }


        private void toReset(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("RESET!");
            Properties.Settings.Default.Reset();
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();

            

        }

        private void toOld(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("OLD");
            settUnvisible.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            settOld.Visibility = Visibility.Visible;
            settOldLoad.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }

        private void toExit(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("Close!");
            this.Close();
        }
    }
}
