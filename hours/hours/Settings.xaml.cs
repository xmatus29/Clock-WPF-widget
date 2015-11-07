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

            MainWindow.I.Opacity = opacity.Value / 10.0;
            
        }

        /* metoda reagujici na zmenu hodnoty baru velikosti */
        private void change_size(object sender, RoutedEventArgs e)
        {
            System.Console.WriteLine("size change on: " + size.Value / 10.0);
            MainWindow.I.Height = size.Value * 100;
            MainWindow.I.Width = size.Value * 100;
            MainWindow.I.resize();

        }

        /*
         * Nastavene aby bolo okno vzdy narvchu
         */
        private void vzdyNavrchuTrue(object sender, RoutedEventArgs e)
        {
            MainWindow.I.Topmost = true;
            MainWindow.I.vzdyNavrchu = true;
        }

        /*
         * Nastavene ze neni okno vzdy navrhu
         */
        private void vzdyNavrchuFalse(object sender, RoutedEventArgs e)
        {
            MainWindow.I.Topmost = false;
            MainWindow.I.vzdyNavrchu = false;
        }
    }
}
