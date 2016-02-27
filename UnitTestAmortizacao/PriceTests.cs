using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AmortizacaoAPOO.Logic;
using AmortizacaoAPOO.Models;

namespace UnitTestAmortizacao
{
    [TestClass]
    public class PriceTests
    {
        Divida d = new Divida(30000, 1.5, 12);
        Price amortizacao = new Price();

        [TestMethod]
        public void PrestacaoDeveSerConstante()
        {
            Divida resultado = amortizacao.Calcular(d);

            foreach (Parcela p in resultado.Parcelas)
            {
                if (p.Periodo != 0)
                {
                    Assert.AreEqual(p.Prestacao, 2750.4);
                }
            }
        }

        [TestMethod]
        public void JurosDeveSerSaldoDevedorDoMesAnteriorMultiplicadoPelaTaxaDeJuros()
        {
            Divida resultado = amortizacao.Calcular(d);
            double esperado;

            for (int i = 1; i < resultado.Parcelas.Count; i++)
            {
                esperado = Math.Round((resultado.Parcelas[i - 1].SaldoDevedor * d.TaxaDeJuros), 2);
                Assert.AreEqual(resultado.Parcelas[i].Juros, esperado);
            }
        }

        [TestMethod]
        public void AmortizacaoDeveSerSubtracaoPrestacaoPeloJuros()
        {
            Divida resultado = amortizacao.Calcular(d);

            foreach (Parcela p in resultado.Parcelas)
            {
                Assert.AreEqual(p.Amortizacao, Math.Round(p.Prestacao - p.Juros), 2);
            }
        }

        [TestMethod]
        public void SaldoDevedorDeveSerIgualMesAnteriorMenosAmortizacaoAtual()
        {
            Divida resultado = amortizacao.Calcular(d);
            double esperado;

            for (int i = 1; i < resultado.Parcelas.Count; i++)
            {
                esperado = Math.Round((resultado.Parcelas[i - 1].SaldoDevedor - resultado.Parcelas[i].Amortizacao), 2);
                Assert.AreEqual(resultado.Parcelas[i].SaldoDevedor, esperado);
            }
        }
    }
}
