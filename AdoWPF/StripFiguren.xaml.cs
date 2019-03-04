﻿using System;
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
using System.Windows.Shapes;
using AdoGemeenschap;

namespace AdoWPF
{
    /// <summary>
    /// Interaction logic for StripFiguren.xaml
    /// </summary>
    public partial class StripFiguren : Window
    {
        private List<Figuur> figuren = new List<Figuur>();
        private CollectionViewSource figuurViewSource;
        public StripFiguren()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            figuurViewSource = ((CollectionViewSource)(this.FindResource("figuurViewSource")));
            FiguurManager manager = new FiguurManager();
            figuren = manager.GetFiguren();
            figuurViewSource.Source = figuren;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var GewijzigdeFiguren = new List<Figuur>();
            foreach (Figuur f in figuren)
            {
                if (f.Changed == true)
                    GewijzigdeFiguren.Add(f);
                f.Changed = false;
            }

            if (GewijzigdeFiguren.Count != 0)
            {
                var manager = new FiguurManager();
                try
                {
                    manager.SchrijfWijzigingen(GewijzigdeFiguren);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            GewijzigdeFiguren.Clear();
        }
    }
}
