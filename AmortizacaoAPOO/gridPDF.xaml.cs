using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
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

namespace AmortizacaoAPOO
{
    /// <summary>
    /// Interaction logic for gridPDF.xaml
    /// </summary>
    public partial class gridPDF : Window
    {
        public gridPDF(double m, double j, int p, int t)
        {
            // m : montate
            // j : juros (%)
            // p : parcelas
            // t : tipo

            InitializeComponent();
                            
            List<Values> users = new List<Values>();
            double jurosDaVez, prestacaoDaVez, amortizacao, amortizacaoDaVez=0, montanteDaVez=0;
            double jurosTotal=0, prestacaoTotal=0, amortizacaoTotal=0;

            // primeira linha
            users.Add(new Values() { Periodo = Convert.ToString(0), Prestacao = "", Juros = "", Amortizacao = "", saldoDevedor = Convert.ToString(m) });    

            switch (t){
                case 0: // SAC     
                    amortizacao = 0;
                    amortizacao = m/p;

                    for (int i = 1; i < p+1; i++) {

                       m -= amortizacao;
                       jurosDaVez = (p - i + 1) * j * amortizacao;
                       prestacaoDaVez = jurosDaVez + amortizacao;

                       jurosTotal += jurosDaVez;
                       prestacaoTotal += prestacaoDaVez;
                       amortizacaoTotal += amortizacao; 

                       users.Add(new Values() { 
                           Periodo = Convert.ToString(i),
                           Prestacao = Convert.ToString(prestacaoDaVez),  // ok
                           Juros = Convert.ToString(jurosDaVez), // ok
                           Amortizacao = Convert.ToString(amortizacao), // ok 
                           saldoDevedor = Convert.ToString(m) // ok
                       }); 
                        
                    }
                    users.Add(new Values() { 
                        Periodo = "TOTAL", 
                        Prestacao = Convert.ToString(prestacaoTotal), 
                        Juros = Convert.ToString(jurosTotal),
                        Amortizacao = Convert.ToString(amortizacaoTotal), 
                        saldoDevedor = "" 
                    });    

                break;

                case 1: // Price
                montanteDaVez = m;
                prestacaoDaVez = m * ((Math.Pow((1 + j), p) * j) /
                                      (Math.Pow((1 + j), p) - 1));    
                
                for (int i = 1; i < p+1; i++) {

                    jurosDaVez = (montanteDaVez * 0.03);
                    amortizacaoDaVez = prestacaoDaVez - jurosDaVez;
                    montanteDaVez = montanteDaVez - amortizacaoDaVez;

                    users.Add(new Values() {
                        Periodo = Convert.ToString(i), // ok
                        Prestacao = prestacaoDaVez.ToString("N2"),  // ok
                        Juros = jurosDaVez.ToString("N2"),  // ok 
                        Amortizacao = amortizacaoDaVez.ToString("N2"), // ok 
                        saldoDevedor = montanteDaVez.ToString("N2") // ok
                    }); 
                }

                break;

                case 2: // Americano     
                jurosDaVez = j*m;   
                
                for (int i = 1; i < p; i++) {

                    jurosTotal += jurosDaVez;
                    users.Add(new Values() {
                        Periodo = Convert.ToString(i),
                        Prestacao = "-",
                        Juros = Convert.ToString(jurosDaVez),
                        Amortizacao = "0", // ok 
                        saldoDevedor = Convert.ToString(m) // ok
                    });         
                }
                jurosTotal += jurosDaVez;
                users.Add(new Values() { 
                    Periodo = "12", 
                    Prestacao = "-", 
                    Juros = Convert.ToString(jurosDaVez), 
                    Amortizacao = Convert.ToString(m), 
                    saldoDevedor = "0" 
                });

                users.Add(new Values() { Periodo = "TOTAL", 
                    Prestacao = "-",
                    Juros = Convert.ToString(jurosTotal), 
                    Amortizacao = Convert.ToString(m), 
                    saldoDevedor = "" 
                });    

                break;                        
   	        }
            
            dgValues.ItemsSource = users;
            
        }
        
        public class Values {
            public string Periodo { get; set; }
            public string Prestacao { get; set; }
            public string Juros { get; set; }
            public string Amortizacao { get; set; }
            public string saldoDevedor { get; set; }

        }
        
        private void txtExportar_Click(object sender, RoutedEventArgs e)
        {
            /*
            MemoryStream lMemoryStream = new MemoryStream();
            Package package = Package.Open(lMemoryStream, FileMode.Create);
            XpsDocument doc = new XpsDocument(package);
            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
            writer.Write(dp);
            doc.Close();
            package.Close();

            var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(lMemoryStream);
            PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, d.FileName, 0);
             */
        }
    
    }
}
