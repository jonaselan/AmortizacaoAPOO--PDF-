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
                _table.AddCell(p.Prestacao.ToString());
                _table.AddCell(p.Juros.ToString());
                _table.AddCell(p.Amortizacao.ToString());
                _table.AddCell(p.SaldoDevedor.ToString());
            }
            _table.AddCell("Total");
            _table.AddCell(_source.TotalPrestacao.ToString());
            _table.AddCell(_source.TotalJuros.ToString());
            _table.AddCell(_source.TotalAmortizacao.ToString());
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
                _table.AddCell(p.Prestacao.ToString());
                _table.AddCell(p.Juros.ToString());
                _table.AddCell(p.Amortizacao.ToString());
                _table.AddCell(p.SaldoDevedor.ToString());
            }
            _table.AddCell("Total");
            _table.AddCell(_source.TotalPrestacao.ToString());
            _table.AddCell(_source.TotalJuros.ToString());
            _table.AddCell(_source.TotalAmortizacao.ToString());
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
                _table.AddCell(p.Prestacao.ToString());
                _table.AddCell(p.Juros.ToString());
                _table.AddCell(p.Amortizacao.ToString());
                _table.AddCell(p.SaldoDevedor.ToString());
            }
            _table.AddCell("Total");
            _table.AddCell(_source.TotalPrestacao.ToString());
            _table.AddCell(_source.TotalJuros.ToString());
            _table.AddCell(_source.TotalAmortizacao.ToString());
            _table.AddCell("-");
        }
    }
}
