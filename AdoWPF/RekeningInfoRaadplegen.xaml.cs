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
    /// Interaction logic for RekeningInfoRaadplegen.xaml
    /// </summary>
    public partial class RekeningInfoRaadplegen : Window
    {
        public RekeningInfoRaadplegen()
        {
            InitializeComponent();
        }

        private void ButtonInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var manager = new RekeningenManager();
                var info = manager.RekeningInfoRaadplegen(textBoxRekeningNr.Text);
                labelSaldo.Content = info.Saldo.ToString("C");
                labelKlantNaam.Content = info.Klantnaam;
                labelStatus.Content = String.Empty;
            }
            catch (Exception ex)
            {
                labelSaldo.Content = String.Empty;
                labelKlantNaam.Content = String.Empty;
                labelStatus.Content = ex.Message;
            }
        }
    }
}
