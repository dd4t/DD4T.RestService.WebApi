using DD4T.ContentModel.Contracts.Configuration;
using DD4T.ContentModel.Contracts.Logging;
using DD4T.ContentModel.Contracts.Providers;
using DD4T.ContentModel.Contracts.Serializing;
using DD4T.Providers.Test;
using DD4T.RestService.WebApi.Controllers;
using DD4T.Serialization;
using DD4T.Utils.Logging;
using Ninject;
using Ninject.Modules;
using System.Net.Http;
using System.Web.Http;

namespace DD4T.RestService.WebApi.Test
{
    public class BaseControllerTest
    {
        protected static BinaryController BinaryController { get; set; }
        protected static PageController PageController { get; set; }
        protected static ComponentPresentationController ComponentPresentationController { get; set; }
        public static void Initialize()
        {
            var kernel = new StandardKernel(new RegistrationModule());
            kernel.Load("DD4T.ContentModel.Contracts");
            kernel.Load("DD4T.RestService.WebApi");
            kernel.Load("DD4T.Providers.Test");

            BinaryController = kernel.Get<BinaryController>();
            PageController = kernel.Get<PageController>();
            ComponentPresentationController = kernel.Get<ComponentPresentationController>();

            BinaryController.Request = new HttpRequestMessage();
            BinaryController.Request.SetConfiguration(new HttpConfiguration());
        }

        public class RegistrationModule : NinjectModule
        {
            public override void Load()
            {
                Bind<IDD4TConfiguration>().To<TestConfiguration>().InSingletonScope();
                Bind<ILogger>().To<NullLogger>().InSingletonScope();
                Bind<IProvidersCommonServices>().To<ProviderCommonServices>().InSingletonScope();
                Bind<IPageProvider>().To<TridionPageProvider>().InSingletonScope();
                Bind<ILinkProvider>().To<TridionLinkProvider>().InSingletonScope();
                Bind<IBinaryProvider>().To<TridionBinaryProvider>().InSingletonScope();
                Bind<IComponentPresentationProvider>().To<TridionComponentPresentationProvider>().InSingletonScope();
                Bind<ISerializerService>().To<JSONSerializerService>().InSingletonScope();
            }
        }
    }
}