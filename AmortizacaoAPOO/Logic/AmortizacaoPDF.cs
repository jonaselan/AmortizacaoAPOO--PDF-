using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmortizacaoAPOO.Models;
using Microsoft.Win32;
using iTextSharp.text.pdf;
using System.IO;

namespace AmortizacaoAPOO.Logic
{
    public class AmortizacaoPDF
    {
        public void Export(Models.AmortizacaoPDF pdf)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Pdf File |*.pdf";
            if (sfd.ShowDialog().Value == true)
            {
                PdfWriter wri = PdfWriter.GetInstance(pdf.Page, new FileStream(sfd.FileName, FileMode.Create));
                pdf.Page.Open();
                pdf.Page.Add(pdf.Table);
                pdf.Page.Close();
            }
        }
    }
}
