using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmortizacaoAPOO.Interfaces;
using AmortizacaoAPOO.Models;

namespace AmortizacaoAPOO.Logic
{
    public class SAC : IAmortizacao
    {
        public Divida Calcular(Divida d)
        {
            double amortizacaoConstante = d.Montante / (d.Parcelas.Count-1);
            double saldoDevedor = d.Montante;
            d.TotalAmortizacao = 0;
            d.TotalJuros = 0;
            d.TotalPrestacao = 0;

            foreach (Parcela p in d.Parcelas)
            {
                p.SaldoDevedor = saldoDevedor;
                saldoDevedor -= amortizacaoConstante;
                if (p.Periodo == 0) { continue; }
                p.Amortizacao = amortizacaoConstante;
                p.Juros = Math.Round((((d.Parcelas.Count-1) - p.Periodo + 1) * d.TaxaDeJuros * amortizacaoConstante), 2);
                p.Prestacao = amortizacaoConstante + p.Juros;

                d.TotalAmortizacao += p.Amortizacao;
                d.TotalJuros += p.Juros;
                d.TotalPrestacao += p.Prestacao;
            }

            return d;
        }

    }
}
