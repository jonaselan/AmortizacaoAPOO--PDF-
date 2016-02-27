using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmortizacaoAPOO.Models;

namespace AmortizacaoAPOO.Interfaces
{
    interface IAmortizacao
    {
        Divida Calcular(Divida d);
    }
}
