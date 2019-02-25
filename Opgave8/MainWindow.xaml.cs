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

namespace Opgave8
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

        public List<Soort> soortenOb = new List<Soort>();
        public List<Plant> plantenOb = new List<Plant>();

        private void VulGrid()
        {
            CollectionViewSource soortViewSource = ((CollectionViewSource)(this.FindResource("soortViewSource")));
            CollectionViewSource plantViewSource = ((CollectionViewSource)(this.FindResource("plantViewSource")));
            var manager = new SoortenManager();
            try
            {
                soortenOb = manager.GetSoorten();
                soortViewSource.Source = soortenOb;
                plantenOb = manager.GetPlanten((Int32)comboBoxSoort.SelectedValue);
                plantViewSource.Source = plantenOb;
                labelStatus.Content = "";
            }
            catch (Exception ex)
            {
                labelStatus.Content = ex.Message;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VulGrid();
            comboBoxSoort.Focus();
        }
    }
}
