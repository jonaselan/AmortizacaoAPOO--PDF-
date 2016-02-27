using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AmortizacaoAPOO.Logic;
using AmortizacaoAPOO.Models;
using System.Collections.Generic;

namespace UnitTestAmortizacao
{
    [TestClass]
    public class SACTests
    {
        Divida d = new Divida(120000, 1, 12);
        SAC amortizacao = new SAC();

        [TestMethod]
        public void AmortizacaoDeveSerConstante()
        {
            Divida resultado = amortizacao.Calcular(d);

            foreach(Parcela p in resultado.Parcelas)
            {
                if(p.Periodo != 0)
                {
                    Assert.AreEqual(p.Amortizacao, 10000);
                }
            }
        }

        [TestMethod]
        public void JurosDevemDecrescerCorretamente()
        {
            Divida resultado = amortizacao.Calcular(d);
            List<double> jurosCorretos = new List<double>(){ 1200, 1100, 1000, 900, 800, 700, 600, 500, 400, 300, 200, 100 };
            List<double> juros = new List<double>();
            foreach(Parcela p in resultado.Parcelas)
            {
                if(p.Periodo != 0)
                {
                    juros.Add(p.Juros);
                }
            }

            CollectionAssert.AreEqual(juros, jurosCorretos);
        }

        [TestMethod]
        public void PrestacaoDeveSerASomaDaAmortizacaoComJuros()
        {
            Divida resultado = amortizacao.Calcular(d);
            foreach (Parcela p in resultado.Parcelas)
            {
                if (p.Periodo != 0)
                {
                    Assert.AreEqual(p.Prestacao, p.Amortizacao + p.Juros);
                }
            }
        }

        [TestMethod]
        public void SaldoDevedorDeveDecrescerDeAcordoComAmortizacao()
        {
            Divida resultado = amortizacao.Calcular(d);
            List<double> saldoDevedorCorreto = new List<double>() { 120000, 110000, 100000, 90000, 80000, 70000, 60000, 50000, 40000, 30000, 20000, 10000, 0 };
            List<double> saldoDevedor = new List<double>();
            foreach (Parcela p in resultado.Parcelas)
            {
                saldoDevedor.Add(p.SaldoDevedor);
            }

            CollectionAssert.AreEqual(saldoDevedor, saldoDevedorCorreto);
        }
    }
}
