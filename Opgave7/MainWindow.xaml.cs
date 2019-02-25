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

namespace Opgave7
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

        private void ButtonToevoegen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var manager = new LeveranciersManager();
                labelStatus.Content = "Leverancier met nummer " + manager.Toevoegen(txtBoxNaam.Text, txtBoxAdres.Text, txtBoxPostcode.Text, txtBoxPlaats.Text).ToString() + " is toegevoegd";
            }
            catch (Exception ex)
            {
                labelStatus.Content = ex.Message;
            }
        }

        private void ButtonEindejaarskorting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var manager = new PlantenManager();
                labelStatus.Content = manager.Eindejaarskorting(25) + " records gewijzigd";
            }
            catch (Exception ex)
            {
                labelStatus.Content = ex.Message;
            }
        }

        private void ButtonVervangLeverancier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var manager = new LeveranciersManager();
                manager.VervangLeverancier(2, 3);
                labelStatus.Content = "Leverancier 2 is verwijderd en vervangen door 3";
            }
            catch (Exception ex)
            {
                labelStatus.Content = ex.Message;
            }
        }
    }
}
