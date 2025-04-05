using JurosCompostos.AppServices.Interfaces;
using JurosCompostos.AppServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JurosCompostos.Web.Controllers
{
    public class CalculadoraJurosCompostosController : Controller
    {
        private readonly ICalculadoraService _calculadoraService;
        public CalculadoraJurosCompostosController(ICalculadoraService calculadoraService)
        {
            _calculadoraService = calculadoraService;
        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Calcular(string principal, string taxa, int periodo, string aporte)
        {
            try
            {
                var cultura = new System.Globalization.CultureInfo("pt-BR");

                decimal capitalInicial = decimal.Parse(principal, cultura);
                decimal taxaAnual = decimal.Parse(taxa, cultura);
                decimal aporteMensal = decimal.Parse(aporte, cultura);

                decimal montante = _calculadoraService.CalcularMontante(capitalInicial, taxaAnual /100, periodo, aporteMensal);

                ViewBag.Principal = principal;
                ViewBag.Taxa = taxa;
                ViewBag.Periodo = periodo;
                ViewBag.Aporte = aporte;

                ViewBag.Resultado = montante.ToString("C", cultura);
            }
            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
            }

            return View("Index");
        }
        private decimal ConverterParaDecimal(string valor)
        {
            return decimal.Parse(
                valor.Replace(".", "").Replace(",", "."),
                System.Globalization.CultureInfo.InvariantCulture
            );
        }
    }
}