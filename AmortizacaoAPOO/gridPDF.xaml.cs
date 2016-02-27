﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
﻿using sharpPDF;
using sharpPDF.Enumerators;
using System.Data;
using Microsoft.Win32;
using AmortizacaoAPOO.Models;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AmortizacaoAPOO
{
    /// <summary>
    /// Interaction logic for gridPDF.xaml
    /// </summary>
    public partial class gridPDF : Window
    {

        AmortizacaoPDF _pdf;
        public gridPDF(Models.AmortizacaoPDF pdf)
        {
            InitializeComponent();
            _pdf = pdf;
            dgValues.ItemsSource = pdf.Source.Parcelas;
        }

        private void txtExportar_Click(object sender, RoutedEventArgs e)
        {
            Logic.AmortizacaoPDF amortizacao = new Logic.AmortizacaoPDF();
            amortizacao.Export(_pdf);
        }

    }
}