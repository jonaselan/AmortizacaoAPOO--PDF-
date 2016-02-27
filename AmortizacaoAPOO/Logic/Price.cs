using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmortizacaoAPOO.Models;
using AmortizacaoAPOO.Interfaces;

namespace AmortizacaoAPOO.Logic
{
    class Price : IAmortizacao
    {
        public Divida Calcular(Divida d)
        {
            double prestacao = Math.Round(d.Montante * ((Math.Pow((d.TaxaDeJuros + 1), (d.Parcelas.Count - 1)) * d.TaxaDeJuros) / ((Math.Pow((1 + d.TaxaDeJuros), (d.Parcelas.Count - 1))) - 1)), 2);
            d.Parcelas[0].SaldoDevedor = d.Montante;

            for(int i = 1; i < d.Parcelas.Count; i++)
            {
                d.Parcelas[i].Prestacao = prestacao;
                d.Parcelas[i].Juros = Math.Round((d.Parcelas[i - 1].SaldoDevedor * d.TaxaDeJuros), 2);
                d.Parcelas[i].Amortizacao = Math.Round((d.Parcelas[i].Prestacao - d.Parcelas[i].Juros), 2);
                d.Parcelas[i].SaldoDevedor = Math.Round((d.Parcelas[i - 1].SaldoDevedor - d.Parcelas[i].Amortizacao), 2);

                d.TotalAmortizacao += d.Parcelas[i].Amortizacao;
                d.TotalJuros += d.Parcelas[i].Juros;
                d.TotalPrestacao += d.Parcelas[i].Prestacao;
            }

            return d;
        }
    }
}
