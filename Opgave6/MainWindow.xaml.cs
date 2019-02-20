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
using ConnectieDLL;

namespace Opgave6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonOpzoeken_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var manager = new PlantenManager();
                var info = manager.PlantInfoRaadplegen(Convert.ToInt32(txtBoxSoort.Text));
                labelNaam.Content = info.PlantNaam;
                labelSoort.Content = info.Soort;
                labelLeverancier.Content = info.Leverancier;
                labelKleur.Content = info.Kleur;
                labelKostprijs.Content = info.Kostprijs.ToString("C");
                labelStatus.Content = String.Empty;
            }
            catch (Exception ex)
            {
                labelNaam.Content = String.Empty;
                labelSoort.Content = String.Empty;
                labelLeverancier.Content = String.Empty;
                labelKleur.Content = String.Empty;
                labelKostprijs.Content = String.Empty;
                labelStatus.Content = ex.Message;
            }
        }
    }
}
