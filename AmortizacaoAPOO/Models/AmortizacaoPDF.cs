using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using AmortizacaoAPOO.Logic;
using System.Globalization;

namespace AmortizacaoAPOO.Models
{
    public class AmortizacaoPDF
    {
        Divida _source;
        Document _document;
        PdfPTable _table;
        Type _tipo;

        public Document Page { get { return _document; } }
        public PdfPTable Table { get { return _table; } }
        public Type Tipo { set { _tipo = value; } get { return _tipo; } }
        public Divida Source
        {
            get { return _source; }
            set
            {
                _source = value;
                ChangeDocument();
            }
        }

        public AmortizacaoPDF(Type tipo)
        {
            _tipo = tipo;
        }

        private void ChangeDocument()
        {
            _document = new Document(PageSize.A4);
            _document.SetMargins(40, 40, 40, 40);
            _document.AddCreationDate();
            if (_tipo == typeof(SAC))
            {
                ChangeTableSac();
            }
            else if (_tipo == typeof(Price))
            {
                ChangeTablePrice();
            }
            else
            {
                ChangeTableAmericano();
            }
        }

        private void ChangeTableSac()
        {
            _table = new PdfPTable(5);
            _table.TotalWidth = 531;
            _table.LockedWidth = true;
            PdfPCell cell = new PdfPCell(new Phrase("Tabela SAC"));
            cell.Colspan = 5;
            cell.HorizontalAlignment = 1;
            _table.AddCell(cell);
            _table.AddCell("Período");
            _table.AddCell("Prestação");
            _table.AddCell("Juros");
            _table.AddCell("Amortização");
            _table.AddCell("Saldo Devedor");
            foreach(Parcela p in _source.Parcelas)
            {
                _table.AddCell(p.Periodo.ToString());
                _table.AddCell(String.Format(CultureInfo.CurrentCulture, "{0:C2}", p.Prestacao));
                _table.AddCell(String.Format(CultureInfo.CurrentCulture, "{0:C2}", p.Juros));
                _table.AddCell(String.Format(CultureInfo.CurrentCulture, "{0:C2}", p.Amortizacao));
                _table.AddCell(String.Format(CultureInfo.CurrentCulture, "{0:C2}", p.SaldoDevedor));
            }
            _table.AddCell("Total");
            _table.AddCell(String.Format(CultureInfo.CurrentCulture, "{0:C2}", _source.TotalPrestacao));
            _table.AddCell(String.Format(CultureInfo.CurrentCulture, "{0:C2}", _source.TotalJuros));
            _table.AddCell((String.Format(CultureInfo.CurrentCulture, "{0:C2}", _source.TotalAmortizacao)));
            _table.AddCell("-");
        }
        private void ChangeTablePrice()
        {
            _table = new PdfPTable(5);
            PdfPCell cell = new PdfPCell(new Phrase("Tabela Price"));
            cell.Colspan = 5;
            cell.HorizontalAlignment = 1;
            _table.AddCell(cell);
            _table.AddCell("Período");
            _table.AddCell("Prestação");
            _table.AddCell("Juros");
            _table.AddCell("Amortização");
            _table.AddCell("Saldo Devedor");
            foreach (Parcela p in _source.Parcelas)
            {
                _table.AddCell(p.Periodo.ToString());
                _table.AddCell(String.Format(CultureInfo.CurrentCulture, "{0:C2}", p.Prestacao));
                _table.AddCell(String.Format(CultureInfo.CurrentCulture, "{0:C2}", p.Juros));
                _table.AddCell(String.Format(CultureInfo.CurrentCulture, "{0:C2}", p.Amortizacao));
                _table.AddCell(String.Format(CultureInfo.CurrentCulture, "{0:C2}", p.SaldoDevedor));
            }
            _table.AddCell("Total");
            _table.AddCell(String.Format(CultureInfo.CurrentCulture, "{0:C2}", _source.TotalPrestacao));
            _table.AddCell(String.Format(CultureInfo.CurrentCulture, "{0:C2}", _source.TotalJuros));
            _table.AddCell((String.Format(CultureInfo.CurrentCulture, "{0:C2}", _source.TotalAmortizacao)));
            _table.AddCell("-");
        }
        private void ChangeTableAmericano()
        {
            _table = new PdfPTable(5);
            PdfPCell cell = new PdfPCell(new Phrase("Tabela Americano"));
            cell.Colspan = 5;
            cell.HorizontalAlignment = 1;
            _table.AddCell(cell);
            _table.AddCell("Período");
            _table.AddCell("Prestação");
            _table.AddCell("Juros");
            _table.AddCell("Amortização");
            _table.AddCell("Saldo Devedor");
            foreach (Parcela p in _source.Parcelas)
            {
                _table.AddCell(p.Periodo.ToString());
                _table.AddCell(String.Format(CultureInfo.CurrentCulture, "{0:C2}", p.Prestacao));
                _table.AddCell(String.Format(CultureInfo.CurrentCulture, "{0:C2}", p.Juros));
                _table.AddCell(String.Format(CultureInfo.CurrentCulture, "{0:C2}", p.Amortizacao));
                _table.AddCell(String.Format(CultureInfo.CurrentCulture, "{0:C2}", p.SaldoDevedor));
            }
            _table.AddCell("Total");
            _table.AddCell(String.Format(CultureInfo.CurrentCulture, "{0:C2}", _source.TotalPrestacao));
            _table.AddCell(String.Format(CultureInfo.CurrentCulture, "{0:C2}", _source.TotalJuros));
            _table.AddCell((String.Format(CultureInfo.CurrentCulture, "{0:C2}", _source.TotalAmortizacao)));
            _table.AddCell("-");
        }
    }
}
