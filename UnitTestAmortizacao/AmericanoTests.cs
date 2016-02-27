using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AmortizacaoAPOO.Models;
using AmortizacaoAPOO.Logic;

namespace UnitTestAmortizacao
{
    [TestClass]
    public class AmericanoTests
    {
        Divida d = new Divida(50000, 3, 10);
        Americano amortizacao = new Americano();

        [TestMethod]
        public void JurosDevemSerConstantes()
        {
            Divida resultado = amortizacao.Calcular(d);
            foreach (Parcela p in resultado.Parcelas)
            {
                if(p.Periodo != 0)
                {
                    Assert.AreEqual(p.Juros, 1500);
                }
            }
        }

        [TestMethod]
        public void PrestacaoDeveSerIgualAosJurosAtePenultimaParcela()
        {
            Divida resultado = amortizacao.Calcular(d);
            foreach (Parcela p in resultado.Parcelas)
            {
                if (p.Periodo != resultado.Parcelas.Count - 1)
                {
                    Assert.AreEqual(p.Prestacao, p.Juros);
                }
                else
                {
                    Assert.AreNotEqual(p.Prestacao, p.Juros);
                }
            }
        }

        [TestMethod]
        public void QuitacaoSomenteNoUltimoMes()
        {
            Divida resultado = amortizacao.Calcular(d);
            foreach(Parcela p in resultado.Parcelas)
            {
                if(p.Periodo != resultado.Parcelas.Count - 1)
                {
                    Assert.AreEqual(p.Amortizacao, 0);
                    Assert.AreEqual(p.SaldoDevedor, 50000);
                }
                else
                {
                    Assert.AreEqual(p.SaldoDevedor, 0);
                    Assert.AreEqual(p.Amortizacao, 50000);
                    Assert.AreEqual(p.Prestacao, p.Amortizacao + p.Juros);
                }
            }
        }
    }
}
