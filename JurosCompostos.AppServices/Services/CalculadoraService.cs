using JurosCompostos.AppServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurosCompostos.AppServices.Services
{
    public class CalculadoraService : ICalculadoraService
    {
        public decimal CalcularMontante(decimal capitalInicial, decimal taxaAnual, int periodos, decimal aporteMensal)
        {
            if (capitalInicial < 0)
                throw new ArgumentException("Capital inicial não pode ser negativo.");
            if (taxaAnual < 0)
                throw new ArgumentException("Taxa de juros não pode ser negativa.");
            if (periodos < 0)
                throw new ArgumentException("Número de períodos não pode ser negativo.");
            if (aporteMensal < 0)
                throw new ArgumentException("Aporte mensal não pode ser negativo.");

            decimal taxaMensal = taxaAnual / 12;

            decimal montanteCapitalInicial = capitalInicial * (decimal)Math.Pow(1 + (double)taxaMensal, periodos);
            decimal montanteAportes = aporteMensal * ((decimal)Math.Pow(1 + (double)taxaMensal, periodos) - 1) / taxaMensal;

            return montanteCapitalInicial + montanteAportes;
        }
    }
}
