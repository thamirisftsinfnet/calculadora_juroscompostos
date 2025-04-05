using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurosCompostos.AppServices.Interfaces
{
    public interface ICalculadoraService
    {
        decimal CalcularMontante(decimal capitalInicial, decimal taxaAnual, int periodos, decimal aporteMensal);
    }
}
