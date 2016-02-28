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
using System.Globalization;
using AmortizacaoAPOO.Models;
using AmortizacaoAPOO.Logic;
using AmortizacaoAPOO.Interfaces;

namespace AmortizacaoAPOO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cbTipo.SelectedIndex = 0;
        }

        private void CreateTxt_Click(object sender, RoutedEventArgs e)
        {
            Divida divida = new Divida(double.Parse(txtMontante.Text, NumberStyles.Currency), double.Parse(txtJuros.Text.Replace("%", "")), int.Parse(txtNParcelas.Text));
            IAmortizacao amortizacao = null;
            Models.AmortizacaoPDF pdf = null;
            string _title = "Tabela";

            switch (cbTipo.SelectedIndex)
            {
                case 0:
                    amortizacao = new SAC();
                    pdf = new Models.AmortizacaoPDF(typeof(SAC));
                    _title = "Tabela SAC";
                    break;
                case 1:
                    amortizacao = new Price();
                    pdf = new Models.AmortizacaoPDF(typeof(Price));
                    _title = "Tabela Price";
                    break;
                case 2:
                    amortizacao = new Americano();
                    pdf = new Models.AmortizacaoPDF(typeof(Americano));
                    _title = "Tabela Americano";
                    break;
            }

            divida = amortizacao.Calcular(divida);
            pdf.Source = divida;

            gridPDF grid = new gridPDF(pdf);
            grid.Title = _title;
            grid.Show();
        }

        private void txtMontante_LostFocus(object sender, RoutedEventArgs e)
        {
            Double value;
            if (Double.TryParse(txtMontante.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out value))
            {
                txtMontante.Text = String.Format(CultureInfo.CurrentCulture, "{0:C2}", value);
            }
            else
            {
                txtMontante.Text = String.Empty;
            }
        }

        private void txtJuros_LostFocus(object sender, RoutedEventArgs e)
        {
            Double value;
            if (Double.TryParse(txtJuros.Text.Replace("%", ""), NumberStyles.Any, CultureInfo.CurrentCulture, out value))
            {
                txtJuros.Text = String.Format("{0:#}%", value);
            }
            else
            {
                txtJuros.Text = String.Empty;
            }
        }

        public void checkBtnGerarTable()
        {
            Double value;
            int valueInt;

            bool montante = Double.TryParse(txtMontante.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out value);
            bool juros = Double.TryParse(txtJuros.Text.Replace("%", ""), NumberStyles.Any, CultureInfo.CurrentCulture, out value);
            bool nParcelas = Int32.TryParse(txtNParcelas.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out valueInt);

            if (montante && juros && nParcelas) { CreateTxt.IsEnabled = true; }
            else { CreateTxt.IsEnabled = false; }
        }

        private void txtMontante_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkBtnGerarTable();
        }

        private void txtJuros_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkBtnGerarTable();
        }

        private void txtNParcelas_TextChanged(object sender, TextChangedEventArgs e)
        {
            checkBtnGerarTable();
        }
    }
}
