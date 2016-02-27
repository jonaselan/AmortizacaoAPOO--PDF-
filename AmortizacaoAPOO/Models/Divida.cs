using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmortizacaoAPOO.Models
{
    public class Divida
    {
        List<Parcela> _parcelas;
        double _montante;
        double _taxaDeJuros;
        double _totalPrestacao;
        double _totalJuros;
        double _totalAmortizacao;

        public double Montante { get { return _montante; } set { _montante = value; } }
        public double TaxaDeJuros { get { return _taxaDeJuros; } set { _taxaDeJuros = value; } }
        public List<Parcela> Parcelas { get { return _parcelas; } }
        public double TotalPrestacao { get { return _totalPrestacao; } set { _totalPrestacao = value; } }
        public double TotalJuros { get { return _totalJuros; } set { _totalJuros = value; } }
        public double TotalAmortizacao { get { return _totalAmortizacao; } set { _totalAmortizacao = value; } }

        public Divida(double montante, double juros, int nParcelas)
        {
            _montante = montante;
            _taxaDeJuros = juros / 100;
            _parcelas = new List<Parcela>();

            for (int i = 0; i <= nParcelas; i++)
            {
                _parcelas.Add(new Parcela(i));
            }
        }
    }
}
