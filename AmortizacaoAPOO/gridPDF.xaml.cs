using System;
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

namespace AmortizacaoAPOO
{
    /// <summary>
    /// Interaction logic for gridPDF.xaml
    /// </summary>
    public partial class gridPDF : Window
    {
        /*Table's creation*/
        pdfTable myTable = new pdfTable();
        pdfDocument myDoc = new pdfDocument("Sample Application", "Me", false);
        pdfPage myFirstPage;

        public gridPDF(double m, double j, int p, int t)
        {
            // m : montate
            // j : juros (%)
            // p : parcelas
            // t : tipo

            InitializeComponent();

            myFirstPage = myDoc.addPage();
            myFirstPage.addText("TABELA AMORTIZAÇÃO", 100, 660, predefinedFont.csHelveticaOblique, 30, new pdfColor(predefinedColor.csCyan));

            //Set table's border
            //myTable.borderSize = 1;
            //myTable.borderColor = new pdfColor(predefinedColor.csDarkBlue);

            /*Create table's header*/
            myTable.tableHeader.addColumn(new pdfTableColumn("", predefinedAlignment.csCenter, 20));
            myTable.tableHeader.addColumn(new pdfTableColumn("Prestação (R$)", predefinedAlignment.csCenter, 100));
            myTable.tableHeader.addColumn(new pdfTableColumn("Juros (R$)", predefinedAlignment.csCenter, 100));
            myTable.tableHeader.addColumn(new pdfTableColumn("Amortização (R$)", predefinedAlignment.csCenter, 100));
            myTable.tableHeader.addColumn(new pdfTableColumn("Saldo devedor(R$)", predefinedAlignment.csCenter, 100));

            /*Create table's rows*/
            pdfTableRow myRow;

            List<Values> users = new List<Values>();
            double jurosDaVez, prestacaoDaVez, amortizacao, amortizacaoDaVez = 0, montanteDaVez = 0;
            double jurosTotal = 0, prestacaoTotal = 0, amortizacaoTotal = 0;

            // primeira linha
            users.Add(new Values() { Periodo = Convert.ToString(0), Prestacao = "", Juros = "", Amortizacao = "", saldoDevedor = Convert.ToString(m) });
            myRow = myTable.createRow();
            myRow[0].columnValue = "";
            myRow[1].columnValue = "";
            myRow[2].columnValue = "";   
            myRow[3].columnValue = "";   
            myRow[4].columnValue = Convert.ToString(m);
            myTable.addRow(myRow);

            switch (t)
            {
                case 0: // SAC     
                    amortizacao = 0;
                    amortizacao = m / p;

                    for (int i = 1; i < p + 1; i++)
                    {
                        m -= amortizacao;
                        jurosDaVez = (p - i + 1) * j * amortizacao;
                        prestacaoDaVez = jurosDaVez + amortizacao;

                        jurosTotal += jurosDaVez;
                        prestacaoTotal += prestacaoDaVez;
                        amortizacaoTotal += amortizacao;

                        users.Add(new Values()
                        {
                            Periodo = Convert.ToString(i),
                            Prestacao = Convert.ToString(prestacaoDaVez),  
                            Juros = Convert.ToString(jurosDaVez), 
                            Amortizacao = Convert.ToString(amortizacao),  
                            saldoDevedor = Convert.ToString(m) 
                        });
                        myRow = myTable.createRow();
                        myRow[0].columnValue = Convert.ToString(i);
                        myRow[1].columnValue = Convert.ToString(prestacaoDaVez);  
                        myRow[2].columnValue = Convert.ToString(jurosDaVez);  
                        myRow[3].columnValue = Convert.ToString(amortizacao);
                        myRow[4].columnValue = Convert.ToString(m);
                        myTable.addRow(myRow);
                    }
                    
                    // ultima linha
                    users.Add(new Values()
                    {
                        Periodo = "TOTAL",
                        Prestacao = Convert.ToString(prestacaoTotal),
                        Juros = Convert.ToString(jurosTotal),
                        Amortizacao = Convert.ToString(amortizacaoTotal),
                        saldoDevedor = ""
                    });
                        
                    myRow = myTable.createRow();
                    myRow[0].columnValue = "T";
                    myRow[1].columnValue = Convert.ToString(prestacaoTotal);
                    myRow[2].columnValue = Convert.ToString(jurosTotal);
                    myRow[3].columnValue = Convert.ToString(amortizacaoTotal);
                    myRow[4].columnValue = "";
                    myTable.addRow(myRow);

                    break;

                case 1: // Price
                    montanteDaVez = m;
                    prestacaoDaVez = m * ((Math.Pow((1 + j), p) * j) /
                                          (Math.Pow((1 + j), p) - 1));

                    for (int i = 1; i < p + 1; i++)
                    {

                        jurosDaVez = (montanteDaVez * 0.03);
                        amortizacaoDaVez = prestacaoDaVez - jurosDaVez;
                        montanteDaVez = montanteDaVez - amortizacaoDaVez;

                        users.Add(new Values()
                        {
                            Periodo = Convert.ToString(i), 
                            Prestacao = prestacaoDaVez.ToString("N2"),  
                            Juros = jurosDaVez.ToString("N2"),   
                            Amortizacao = amortizacaoDaVez.ToString("N2"),  
                            saldoDevedor = montanteDaVez.ToString("N2") 
                        });

                        myRow = myTable.createRow();
                        myRow[0].columnValue = Convert.ToString(i);
                        myRow[1].columnValue = prestacaoDaVez.ToString("N2"); 
                        myRow[2].columnValue = jurosDaVez.ToString("N2");   
                        myRow[3].columnValue = amortizacaoDaVez.ToString("N2");   
                        myRow[4].columnValue = montanteDaVez.ToString("N2");
                        myTable.addRow(myRow);

                    }

                    break;

                case 2: // Americano     
                    jurosDaVez = j * m;

                    for (int i = 1; i < p; i++)
                    {
                        jurosTotal += jurosDaVez;
                        users.Add(new Values()
                        {
                            Periodo = Convert.ToString(i),
                            Prestacao = "-",
                            Juros = Convert.ToString(jurosDaVez),
                            Amortizacao = "0",  
                            saldoDevedor = Convert.ToString(m) 
                        });

                        myRow = myTable.createRow();
                        myRow[0].columnValue = Convert.ToString(i);
                        myRow[1].columnValue = "-";
                        myRow[2].columnValue = Convert.ToString(jurosDaVez);
                        myRow[3].columnValue = "0";
                        myRow[4].columnValue = Convert.ToString(m);
                        myTable.addRow(myRow);

                    }

                    jurosTotal += jurosDaVez;
                    users.Add(new Values()
                    {
                        Periodo = "12",
                        Prestacao = "-",
                        Juros = Convert.ToString(jurosDaVez),
                        Amortizacao = Convert.ToString(m),
                        saldoDevedor = "0"
                    });
                    myRow = myTable.createRow();
                        myRow[0].columnValue = "12";
                        myRow[1].columnValue = "-";
                        myRow[2].columnValue = Convert.ToString(jurosDaVez);
                        myRow[3].columnValue = Convert.ToString(m);
                        myRow[4].columnValue = "0";
                        myTable.addRow(myRow);


                    users.Add(new Values()
                    {
                        Periodo = "TOTAL",
                        Prestacao = "-",
                        Juros = Convert.ToString(jurosTotal),
                        Amortizacao = Convert.ToString(m),
                        saldoDevedor = ""
                    });
                    myRow = myTable.createRow();
                        myRow[0].columnValue = "T";
                        myRow[1].columnValue = "-";
                        myRow[2].columnValue = Convert.ToString(jurosTotal);
                        myRow[3].columnValue = Convert.ToString(m);
                        myRow[4].columnValue = "";
                        myTable.addRow(myRow);

                    break;
            }

            dgValues.ItemsSource = users;
        }

        public class Values
        {
            public string Periodo { get; set; }
            public string Prestacao { get; set; }
            public string Juros { get; set; }
            public string Amortizacao { get; set; }
            public string saldoDevedor { get; set; }

        }

        private void txtExportar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /*Set Header's Style*/
                // myTable.tableHeaderStyle = new pdfTableRowStyle(predefinedFont.csCourierBoldOblique, 12, new pdfColor(predefinedColor.csBlack), new pdfColor(predefinedColor.csLightCyan));

                /*Set Row's Style
                // myTable.rowStyle = new pdfTableRowStyle(predefinedFont.csCourier, 8, new pdfColor(predefinedColor.csBlack), new pdfColor(predefinedColor.csWhite));
                
                /*Set Alternate Row's Style*/
                // myTable.alternateRowStyle = new pdfTableRowStyle(predefinedFont.csCourier, 8, new pdfColor(predefinedColor.csBlack), new pdfColor(predefinedColor.csLightYellow));

                /*Set Cellpadding*/
                // myTable.cellpadding = 20;

                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = "tabelaAmortizacao"; // Default file name
                sfd.DefaultExt = ".pdf"; // Default file extension

                if (sfd.ShowDialog() == true)
                {
                    myFirstPage.addTable(myTable, 100, 600);
                    myTable = null;
                    myDoc.createPDF(sfd.FileName);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro: " + e);
            }
            MessageBox.Show("PDF Criado com sucesso!");
        }

    }
}