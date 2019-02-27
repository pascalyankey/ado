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
        public OverzichtBrouwers()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VulDeGrid();
            textBoxZoeken.Focus();
        }

        public List<Brouwer> brouwersOb = new List<Brouwer>();

        private void VulDeGrid()
        {
            brouwerViewSource = (CollectionViewSource)(this.FindResource("brouwerViewSource"));
            var manager = new BrouwerManager();
            brouwersOb = manager.GetBrouwersBeginNaam(textBoxZoeken.Text);
            brouwerViewSource.Source = brouwersOb;
            GoUpdate();
            labelTotalRowCount.Content = brouwerDataGrid.Items.Count;
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
            goToNextButton.IsEnabled = !(brouwerViewSource.View.CurrentPosition == brouwerDataGrid.Items.Count - 1);
            goToLastButton.IsEnabled = !(brouwerViewSource.View.CurrentPosition == brouwerDataGrid.Items.Count - 1);

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
                if (Validation.GetHasError((DependencyObject)c))
                {
                    foutGevonden = true;
                }
            }
            if (foutGevonden)
                e.Handled = true;
        }
    }
}
