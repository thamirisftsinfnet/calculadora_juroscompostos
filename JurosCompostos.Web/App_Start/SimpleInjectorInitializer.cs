using JurosCompostos.AppServices.Interfaces;
using JurosCompostos.AppServices.Services;
using JurosCompostos.Web.App_Start;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(SimpleInjectorInitializer), "Initialize")]
namespace JurosCompostos.Web.App_Start
{
    public class SimpleInjectorInitializer
    {
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());


            container.Verify();


            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            BootStrapper.RegisterServices(container);
        }

    }


    public class BootStrapper
    {
        public static void RegisterServices(Container container)
        {
            container.Register<ICalculadoraService, CalculadoraService>(Lifestyle.Scoped);
        }
    }
}