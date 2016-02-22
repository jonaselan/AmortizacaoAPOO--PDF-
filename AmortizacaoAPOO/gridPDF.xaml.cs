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
        public gridPDF(int m, int j, int p, int t)
        {
            InitializeComponent();
                            
            List<Values> users = new List<Values>();
            users.Add(new Values() { Prestacao = "s", Juros = "sim", Amortizacao="aa", saldoDevedor="ss" });

            dgValues.ItemsSource = users;
         
        }
        
        public class Values
        {
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
