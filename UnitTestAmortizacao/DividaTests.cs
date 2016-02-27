using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AmortizacaoAPOO.Models;

namespace UnitTestAmortizacao
{
    [TestClass]
    public class DividaTests
    {
        [TestMethod]
        public void DeveAtribuirNumeroCorretoDeParcelas()
        {
            Divida d = new Divida(0, 0, 10);
            for(int i = 0; i < d.Parcelas.Count; i++)
            {
                Assert.AreEqual(d.Parcelas[i].Periodo, i);
            }
            Assert.AreEqual(d.Parcelas.Count, 11);
        }
    }
}
