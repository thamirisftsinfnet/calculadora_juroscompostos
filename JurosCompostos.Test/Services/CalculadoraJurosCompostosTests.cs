using NUnit.Framework;
using JurosCompostos.AppServices.Services;
using System;

namespace JurosCompostos.Tests.Services
{
    [TestFixture]
    public class CalculadoraJurosCompostosTests
    {
        private readonly CalculadoraService _calc = new CalculadoraService();

        [Test]
        public void CalcularMontante_ValoresValidos_DeveRetornarMontanteCorreto()
        {
            decimal capitalInicial = 1000m;
            decimal taxaAnual = 0.12m;
            int periodos = 12;
            decimal aporteMensal = 200m;
            decimal taxaMensal = taxaAnual / 12;

            decimal resultado = _calc.CalcularMontante(capitalInicial, taxaAnual, periodos, aporteMensal);

            decimal esperado =
                capitalInicial * (decimal)Math.Pow(1 + (double)taxaMensal, periodos) +
                aporteMensal * ((decimal)Math.Pow(1 + (double)taxaMensal, periodos) - 1) / taxaMensal;

            Assert.That((double)resultado, Is.EqualTo((double)esperado).Within(0.01), "Resultado diferente do esperado.");
        }

        [Test]
        public void CalcularMontante_SemAportes_DeveRetornarSomenteMontanteInicial()
        {
            decimal capitalInicial = 1000m;
            decimal taxaAnual = 0.12m;
            int periodos = 12;
            decimal aporteMensal = 0m;
            decimal taxaMensal = taxaAnual / 12;

            decimal esperado = capitalInicial * (decimal)Math.Pow(1 + (double)taxaMensal, periodos);
            decimal resultado = _calc.CalcularMontante(capitalInicial, taxaAnual, periodos, aporteMensal);

            Assert.That((double)resultado, Is.EqualTo((double)esperado).Within(0.01), "Montante sem aportes incorreto.");
        }

    
        [Test]
        public void CalcularMontante_PeriodoZero_DeveRetornarApenasCapitalInicial()
        {
            decimal capitalInicial = 5000m;
            decimal taxaAnual = 0.1m;
            int periodos = 0;
            decimal aporteMensal = 300m;

            decimal resultado = _calc.CalcularMontante(capitalInicial, taxaAnual, periodos, aporteMensal);

            Assert.That((double)resultado, Is.EqualTo((double)capitalInicial).Within(0.01), "Montante com período zero incorreto.");
        }

        [Test]
        public void CalcularMontante_CapitalNegativo_DeveLancarExcecao()
        {
            Assert.Throws<ArgumentException>(() => _calc.CalcularMontante(-1000m, 0.1m, 12, 100m));
        }

        [Test]
        public void CalcularMontante_TaxaNegativa_DeveLancarExcecao()
        {
            Assert.Throws<ArgumentException>(() => _calc.CalcularMontante(1000m, -0.1m, 12, 100m));
        }

        [Test]
        public void CalcularMontante_PeriodoNegativo_DeveLancarExcecao()
        {
            Assert.Throws<ArgumentException>(() => _calc.CalcularMontante(1000m, 0.1m, -5, 100m));
        }

        [Test]
        public void CalcularMontante_AporteNegativo_DeveLancarExcecao()
        {
            Assert.Throws<ArgumentException>(() => _calc.CalcularMontante(1000m, 0.1m, 12, -100m));
        }
    }
}
