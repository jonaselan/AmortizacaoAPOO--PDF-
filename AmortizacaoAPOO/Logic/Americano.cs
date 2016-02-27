using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmortizacaoAPOO.Models;
using AmortizacaoAPOO.Interfaces;

namespace AmortizacaoAPOO.Logic
{
    public class Americano : IAmortizacao
    {
        public Divida Calcular(Divida d)
        {
            double saldoDevedor = d.Montante;
            foreach(Parcela p in d.Parcelas)
            {
                if (p.Periodo != (d.Parcelas.Count - 1)) { p.SaldoDevedor = saldoDevedor; }
                if(p.Periodo == 0) { continue; }
                p.Juros = Math.Round((saldoDevedor * d.TaxaDeJuros), 2);
                if(p.Periodo == d.Parcelas.Count - 1)
                {
                    p.Amortizacao = saldoDevedor;
                    p.Prestacao = p.Amortizacao + p.Juros;
                }
                else
                {
                    p.Prestacao = p.Juros;
                }
                d.TotalAmortizacao += p.Amortizacao;
                d.TotalJuros += p.Juros;
                d.TotalPrestacao += p.Prestacao;
            }
            return d;
        }
    }
}
