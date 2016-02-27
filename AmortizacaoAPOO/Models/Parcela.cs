using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmortizacaoAPOO.Models
{
    public class Parcela
    {
        int _periodo;
        double _prestacao;
        double _juros;
        double _amortizacao;
        double _saldoDevedor;

        public int Periodo { get { return _periodo; } }
        public double Prestacao { get { return _prestacao; } set { _prestacao = value; } }
        public double Juros { get { return _juros; } set { _juros = value; } }
        public double Amortizacao { get { return _amortizacao; } set { _amortizacao = value; } }
        public double SaldoDevedor { get { return _saldoDevedor; } set { _saldoDevedor = value; } }

        public Parcela(int periodo)
        {
            _periodo = periodo;
        }
    }
}
