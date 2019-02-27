﻿using System;
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
using System.Windows.Shapes;
using AdoGemeenschap;

namespace AdoWPF
{
    /// <summary>
    /// Interaction logic for OverzichtBrouwers.xaml
    /// </summary>
    public partial class OverzichtBrouwers : Window
    {
        private CollectionViewSource brouwerViewSource;
        public ObservableCollection<Brouwer> brouwersOb = new ObservableCollection<Brouwer>();
        public List<Brouwer> OudeBrouwers = new List<Brouwer>();
        public List<Brouwer> NieuweBrouwers = new List<Brouwer>();
        public List<Brouwer> GewijzigdeBrouwers = new List<Brouwer>();
        public OverzichtBrouwers()
        {
            InitializeComponent();
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (Brouwer oudeBrouwer in e.OldItems)
                {
                    OudeBrouwers.Add(oudeBrouwer);
                }
            }
            if (e.NewItems != null)
            {
                foreach (Brouwer nieuweBrouwer in e.NewItems)
                {
                    NieuweBrouwers.Add(nieuweBrouwer);
                }
            }
            labelTotalRowCount.Content = brouwerDataGrid.Items.Count;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VulDeGrid();
            textBoxZoeken.Focus();

            var nummers = (from b in brouwersOb orderby b.Postcode select b.Postcode.ToString()).Distinct().ToList();
            nummers.Insert(0, "alles");
            comboBoxPostCode.ItemsSource = nummers;
            comboBoxPostCode.SelectedIndex = 0;
        }

        private void VulDeGrid()
        {
            brouwerViewSource = (CollectionViewSource)(this.FindResource("brouwerViewSource"));
            var manager = new BrouwerManager();
            brouwersOb = manager.GetBrouwersBeginNaam(textBoxZoeken.Text);
            brouwerViewSource.Source = brouwersOb;
            GoUpdate();
            labelTotalRowCount.Content = brouwerDataGrid.Items.Count;
            brouwersOb.CollectionChanged += this.OnCollectionChanged;
        }

        private void ButtonZoeken_Click(object sender, RoutedEventArgs e)
        {
            VulDeGrid();
        }

        private void TextBoxZoeken_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                VulDeGrid();
            }
        }

        private void goToFirstButton_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToFirst();
            GoUpdate();
        }

        private void goToPreviousButton_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToPrevious();
            GoUpdate();
        }

        private void goToNextButton_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToNext();
            GoUpdate();
        }

        private void goToLastButton_Click(object sender, RoutedEventArgs e)
        {
            brouwerViewSource.View.MoveCurrentToLast();
            GoUpdate();
        }

        private void GoUpdate()
        {
            goToPreviousButton.IsEnabled = !(brouwerViewSource.View.CurrentPosition == 0);
            goToFirstButton.IsEnabled = !(brouwerViewSource.View.CurrentPosition == 0);
            goToNextButton.IsEnabled = !(brouwerViewSource.View.CurrentPosition == brouwerDataGrid.Items.Count - 2);
            goToLastButton.IsEnabled = !(brouwerViewSource.View.CurrentPosition == brouwerDataGrid.Items.Count - 2);

            if (brouwerDataGrid.Items.Count != 0)
            {
                if (brouwerDataGrid.SelectedItem != null)
                {
                    brouwerDataGrid.ScrollIntoView(brouwerDataGrid.SelectedItem);
                    listBoxBrouwers.ScrollIntoView(brouwerDataGrid.SelectedItem);
                }
            }
            textBoxGo.Text = (brouwerViewSource.View.CurrentPosition + 1).ToString();
        }

        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            int position;
            int.TryParse(textBoxGo.Text, out position);
            if (position > 0 && position <= brouwerDataGrid.Items.Count)
            {
                brouwerViewSource.View.MoveCurrentToPosition(position - 1);
            }
            else
            {
                MessageBox.Show("The input index is not valid.");
            }
            GoUpdate();
        }

        private void BrouwerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GoUpdate();
        }

        private void CheckBoxPostcode0_Checked(object sender, RoutedEventArgs e)
        {
            Binding binding1 = BindingOperations.GetBinding(postcodeTextBox, TextBox.TextProperty);
            binding1.ValidationRules.Clear();

            var binding2 = (postcodeColumn as DataGridBoundColumn).Binding as Binding;
            binding2.ValidationRules.Clear();

            brouwerDataGrid.RowValidationRules.Clear();

            switch (checkBoxPostcode0.IsChecked)
            {
                case true:
                    binding1.ValidationRules.Add(new PostcodeRangeRule0());
                    binding2.ValidationRules.Add(new PostcodeRangeRule0());
                    brouwerDataGrid.RowValidationRules.Add(new PostcodeRangeRule0());
                    break;
                case false:
                    binding1.ValidationRules.Add(new PostcodeRangeRule());
                    binding2.ValidationRules.Add(new PostcodeRangeRule());
                    brouwerDataGrid.RowValidationRules.Add(new PostcodeRangeRule());
                    break;
                default:
                    binding1.ValidationRules.Add(new PostcodeRangeRule());
                    binding2.ValidationRules.Add(new PostcodeRangeRule());
                    brouwerDataGrid.RowValidationRules.Add(new PostcodeRangeRule());
                    break;
            }
        }

        private void TestOpFouten_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            bool foutGevonden = false;
            foreach (var c in gridDetail.Children)
            {
                if (c is AdornerDecorator)
                {
                    if (Validation.GetHasError(((AdornerDecorator)c).Child))
                    {
                        foutGevonden = true;
                    }
                }
                else if (Validation.GetHasError((DependencyObject)c))
                {
                    foutGevonden = true;
                }
            }

            foreach (var c in brouwerDataGrid.ItemsSource)
            {
                var d = brouwerDataGrid.ItemContainerGenerator.ContainerFromItem(c);
                if (d is DataGridRow)
                {
                    if (Validation.GetHasError(d))
                    {
                        foutGevonden = true;
                    }
                }
            }

            if (foutGevonden)
                e.Handled = true;
        }

        private void ComboBoxPostCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxPostCode.SelectedIndex == 0)
                brouwerDataGrid.Items.Filter = null;
            else
                brouwerDataGrid.Items.Filter = new Predicate<object>(PostCodeFilter);
            GoUpdate();
            labelTotalRowCount.Content = brouwerDataGrid.Items.Count - 1;
        }

        public bool PostCodeFilter(object br)
        {
            Brouwer b = br as Brouwer;
            bool result = (b.Postcode == Convert.ToInt16(comboBoxPostCode.SelectedValue));
            return result;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            brouwerDataGrid.CommitEdit(DataGridEditingUnit.Row, true);
            var resultaatBrouwers = new List<Brouwer>();
            var manager = new BrouwerManager();
            if (OudeBrouwers.Count() != 0)
            {
                resultaatBrouwers = manager.SchrijfVerwijderingen(OudeBrouwers);
                if (resultaatBrouwers.Count > 0)
                {
                    StringBuilder boodschap = new StringBuilder();
                    boodschap.Append("Niet verwijderd: \n");
                    foreach (var b in resultaatBrouwers)
                    {
                        boodschap.Append("Nummer: " + b.BrouwerNr + " : " + b.Naam + " niet\n");
                    }
                    MessageBox.Show(boodschap.ToString());
                }
            }
            MessageBox.Show(OudeBrouwers.Count - resultaatBrouwers.Count + " brouwer(s) verwijderd in de database", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

            resultaatBrouwers.Clear();
            if (NieuweBrouwers.Count() != 0)
            {
                resultaatBrouwers = manager.SchrijfToevoegingen(NieuweBrouwers);
                if (resultaatBrouwers.Count > 0)
                {
                    StringBuilder boodschap = new StringBuilder();
                    boodschap.Append("Niet toegevoegd: \n");
                    foreach (var b in resultaatBrouwers)
                    {
                        boodschap.Append("Nummer: " + b.BrouwerNr + " : " + b.Naam + " niet\n");
                    }
                    MessageBox.Show(boodschap.ToString());
                }
            }
            MessageBox.Show(NieuweBrouwers.Count - resultaatBrouwers.Count + " brouwer(s) toegevoegd aan de database", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

            foreach (Brouwer b in brouwersOb)
            {
                if ((b.Changed == true) && (b.BrouwerNr != 0))
                    GewijzigdeBrouwers.Add(b);
                b.Changed = false;
            }

            resultaatBrouwers.Clear();
            if (GewijzigdeBrouwers.Count() != 0)
            {
                resultaatBrouwers = manager.SchrijfWijzigingen(GewijzigdeBrouwers);
                if (resultaatBrouwers.Count > 0)
                {
                    StringBuilder boodschap = new StringBuilder();
                    boodschap.Append("Niet gewijzigd: \n");
                    foreach (var b in resultaatBrouwers)
                    {
                        boodschap.Append("Nummer: " + b.BrouwerNr + " : " + b.Naam + " niet\n");
                    }
                    MessageBox.Show(boodschap.ToString());
                }
            }
            MessageBox.Show(GewijzigdeBrouwers.Count - resultaatBrouwers.Count + " brouwer(s) gewijzigd in de database", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

            VulDeGrid();

            OudeBrouwers.Clear();
            NieuweBrouwers.Clear();
            GewijzigdeBrouwers.Clear();
        }
    }
}
