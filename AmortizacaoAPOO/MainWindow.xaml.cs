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
        }

        private void CreateTxt_Click(object sender, RoutedEventArgs e)
        {
            Divida divida = new Divida(double.Parse(txtMontante.Text), double.Parse(txtJuros.Text), int.Parse(txtNParcelas.Text));
            IAmortizacao amortizacao = null;
            Models.AmortizacaoPDF pdf = null;

            switch (cbTipo.SelectedIndex)
            {
                case 0:
                    amortizacao = new SAC();
                    pdf = new Models.AmortizacaoPDF(typeof(SAC));
                    break;
                case 1:
                    amortizacao = new Price();
                    pdf = new Models.AmortizacaoPDF(typeof(Price));
                    break;
                case 2:
                    amortizacao = new Americano();
                    pdf = new Models.AmortizacaoPDF(typeof(Americano));
                    break;
            }

            divida = amortizacao.Calcular(divida);
            pdf.Source = divida;

            gridPDF grid = new gridPDF(pdf);
            grid.Show();
        }

    }
}
