using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

namespace Opgave9
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

        public ObservableCollection<Soort> soortenOb = new ObservableCollection<Soort>();
        public ObservableCollection<Plant> plantenOb = new ObservableCollection<Plant>();
        public List<Plant> GewijzigdePlanten = new List<Plant>();

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

        private void VulPlanten()
        {
            CollectionViewSource plantViewSource = ((CollectionViewSource)(this.FindResource("plantViewSource")));
            var manager = new SoortenManager();
            try
            {
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

        private void ComboBoxSoort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VulPlanten();
            comboBoxSoort.Focus();
        }

        private void ListBoxPlanten_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Validation.GetHasError(kleurTextBox) || Validation.GetHasError(prijsTextBox))
                e.Handled = true;
        }

        private void ComboBoxSoort_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Validation.GetHasError(kleurTextBox) || Validation.GetHasError(prijsTextBox))
                e.Handled = true;
        }

        private void OpslaanButton_Click(object sender, RoutedEventArgs e)
        {
            var resultaatPlanten = new List<Plant>();
            var manager = new SoortenManager();

            foreach (Plant p in plantenOb)
            {
                if ((p.Changed == true) && (p.SoortNr != 0))
                    GewijzigdePlanten.Add(p);
                p.Changed = false;
            }

            if (GewijzigdePlanten.Count() != 0)
            {
                if (MessageBox.Show($"Gewijzigde planten van soort '{comboBoxSoort.Text}' opslaan?", "Opslaan", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    resultaatPlanten = manager.SchrijfWijzigingen(GewijzigdePlanten);
                    if (resultaatPlanten.Count > 0)
                    {
                        var boodschap = new StringBuilder();
                        boodschap.Append("Niet gewijzigd: \n");
                        foreach (var p in resultaatPlanten)
                        {
                            boodschap.Append("Nummer: " + p.SoortNr + " : " + p.Naam + " niet\n");
                        }
                        MessageBox.Show(boodschap.ToString());
                    }
                }
            }
            MessageBox.Show(GewijzigdePlanten.Count - resultaatPlanten.Count + " plant(en) gewijzigd in de database", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

            VulPlanten();
            GewijzigdePlanten.Clear();
        }
    }
}
