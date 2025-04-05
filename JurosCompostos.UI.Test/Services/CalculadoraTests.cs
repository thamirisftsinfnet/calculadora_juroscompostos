using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace JurosCompostos.UI.Test.Services
{
    [TestFixture]
    public class CalculadoraTests
    {
        private IWebDriver driver;
        private string url = "https://calculadorajuroscompostos-web-e8bac0hub3bxh3am.brazilsouth-01.azurewebsites.net/";

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("start-maximized");
            driver = new ChromeDriver(options);
        }

        [Test]
        public void CalcularMontante_DeveExibirResultadoCorreto()
        {

            driver.Navigate().GoToUrl(url);

            #region Preencher Campos
            driver.FindElement(By.Id("principal")).SendKeys("1000");
            driver.FindElement(By.Id("taxa")).SendKeys("10");
            driver.FindElement(By.Name("periodo")).SendKeys("12");
            driver.FindElement(By.Id("aporte")).SendKeys("100");
            #endregion

            
            driver.FindElement(By.CssSelector("form button[type='submit']")).Click();

           
            System.Threading.Thread.Sleep(1000);

            var resultadoElemento = driver.FindElement(By.XPath("//div[strong[text()='Montante Final:']]"));
            Assert.IsNotNull(resultadoElemento, "Resultado não foi exibido.");
            Assert.IsTrue(resultadoElemento.Text.Contains("R$"), "Formato do resultado inesperado.");
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }
    }
}
