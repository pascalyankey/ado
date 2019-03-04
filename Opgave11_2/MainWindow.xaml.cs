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

namespace Opgave11_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CollectionViewSource leverancierViewSource;
        public ObservableCollection<Leverancier> leveranciersOb = new ObservableCollection<Leverancier>();
        public List<Leverancier> OudeLeveranciers = new List<Leverancier>();
        public List<Leverancier> NieuweLeveranciers = new List<Leverancier>();
        public List<Leverancier> GewijzigdeLeveranciers = new List<Leverancier>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VulGrid();
            comboBoxPostNrs.Focus();

            var nummers = (from l in leveranciersOb orderby l.PostNr select l.PostNr.ToString()).Distinct().ToList();
            nummers.Insert(0, "alles");
            comboBoxPostNrs.ItemsSource = nummers;
            comboBoxPostNrs.SelectedIndex = 0;
        }

        private void VulGrid()
        {
            leverancierViewSource = (CollectionViewSource)(this.FindResource("leverancierViewSource"));
            var manager = new LeveranciersManager();
            leveranciersOb = manager.GetLeveranciers(Convert.ToString(comboBoxPostNrs.SelectedValue));
            leverancierViewSource.Source = leveranciersOb;
            leveranciersOb.CollectionChanged += this.OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Leverancier oudeLeverancier in e.OldItems)
                {
                    OudeLeveranciers.Add(oudeLeverancier);
                }
            }
            if (e.NewItems != null)
            {
                foreach (Leverancier nieuweLeverancier in e.NewItems)
                {
                    NieuweLeveranciers.Add(nieuweLeverancier);
                }
            }
        }

        private void ComboBoxPostNrs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxPostNrs.SelectedIndex == 0)
                leverancierDataGrid.Items.Filter = null;
            else
                leverancierDataGrid.Items.Filter = new Predicate<object>(PostCodeFilter);
        }

        public bool PostCodeFilter(object lev)
        {
            Leverancier l = lev as Leverancier;
            bool result = (l.PostNr == Convert.ToString(comboBoxPostNrs.SelectedValue));
            return result;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Wilt u alles wegschrijven naar de database ?", "Opslaan", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                leverancierDataGrid.CommitEdit(DataGridEditingUnit.Row, true);
                var resultaatLeveranciers = new List<Leverancier>();
                var manager = new LeveranciersManager();
                var boodschap = new StringBuilder();
                var aantalVerwijderd = 0;
                var aantalToegevoegd = 0;
                var aantalGewijzgid = 0;

                if (OudeLeveranciers.Count() != 0)
                {
                    resultaatLeveranciers = manager.SchrijfVerwijderingen(OudeLeveranciers);
                    if (resultaatLeveranciers.Count > 0)
                    {
                        boodschap.Append("Niet verwijderd: ");
                        foreach (var l in resultaatLeveranciers)
                        {
                            boodschap.Append(l.PostNr + " : " + l.Naam + " ");
                        }
                    }
                }
                aantalVerwijderd = OudeLeveranciers.Count - resultaatLeveranciers.Count;

                resultaatLeveranciers.Clear();
                if (NieuweLeveranciers.Count() != 0)
                {
                    resultaatLeveranciers = manager.SchrijfToevoegingen(NieuweLeveranciers);
                    if (resultaatLeveranciers.Count > 0)
                    {
                        boodschap.Append("\nNiet toegevoegd: ");
                        foreach (var l in resultaatLeveranciers)
                        {
                            boodschap.Append(l.PostNr + " : " + l.Naam + " ");
                        }
                    }
                }
                aantalToegevoegd = NieuweLeveranciers.Count - resultaatLeveranciers.Count;

                resultaatLeveranciers.Clear();
                foreach (Leverancier l in leveranciersOb)
                {
                    if ((l.Changed == true) && (l.LevNr != 0))
                        GewijzigdeLeveranciers.Add(l);
                    l.Changed = false;
                }

                if (GewijzigdeLeveranciers.Count() != 0)
                {
                    resultaatLeveranciers = manager.SchrijfWijzigingen(GewijzigdeLeveranciers);
                    if (resultaatLeveranciers.Count > 0)
                    {
                        boodschap.Append("\nNiet gewijzigd: ");
                        foreach (var l in resultaatLeveranciers)
                        {
                            boodschap.Append(l.PostNr + " : " + l.Naam + " ");
                        }
                    }
                }
                aantalGewijzgid = GewijzigdeLeveranciers.Count - resultaatLeveranciers.Count;

                boodschap.Append("\n\n");
                boodschap.Append(aantalVerwijderd + " leverancier(s) verwijderd in de database\n");
                boodschap.Append(aantalToegevoegd + " leverancier(s) toegevoegd in de database\n");
                boodschap.Append(aantalGewijzgid + " leverancier(s) gewijzgid in de database");

                MessageBox.Show(boodschap.ToString());
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var GewijzigdeLeveranciers = new List<Leverancier>();
            
            foreach (Leverancier l in leveranciersOb)
            {
                if (l.Changed == true)
                    GewijzigdeLeveranciers.Add(l);
                l.Changed = false;
            }

            if (GewijzigdeLeveranciers.Count() != 0)
            {
                var manager = new LeveranciersManager();
                try
                {
                    manager.SchrijfWijzigingenMultiUser(GewijzigdeLeveranciers);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            GewijzigdeLeveranciers.Clear();
        }
    }
}
