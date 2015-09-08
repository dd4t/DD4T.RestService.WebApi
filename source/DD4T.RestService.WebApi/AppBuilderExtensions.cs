using Autofac;
using Autofac.Integration.WebApi;
using DD4T.ContentModel.Contracts.Configuration;
using DD4T.ContentModel.Contracts.Logging;
using DD4T.ContentModel.Contracts.Providers;
using DD4T.ContentModel.Contracts.Resolvers;
using DD4T.DI.Autofac;
using DD4T.Utils;
using DD4T.Utils.Logging;
using DD4T.Utils.Resolver;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace DD4T.RestService.WebApi
{
    public static class AppBuilderExtensions
    {
        public static void UseDD4TWebApi(this IAppBuilder appBuilder)
        {
           
            var config = new HttpConfiguration();
            var container = BuildContainer();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            config.MapHttpAttributeRoutes();

            appBuilder.UseAutofacMiddleware(container);
            appBuilder.UseAutofacWebApi(config);
            appBuilder.UseWebApi(config);
          
        }


        static ILifetimeScope BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(typeof(AppBuilderExtensions).Assembly);

            builder.UseDD4T();
            //builder.RegisterType<DD4TConfiguration>().As<IDD4TConfiguration>().InstancePerLifetimeScope().PreserveExistingDefaults();
            //builder.RegisterType<DefaultPublicationResolver>().As<IPublicationResolver>().InstancePerLifetimeScope().PreserveExistingDefaults();
            //builder.RegisterType<DefaultLogger>().As<ILogger>().InstancePerLifetimeScope().PreserveExistingDefaults();
            //builder.RegisterType<TridionPageProvider>().As<IPageProvider>().InstancePerLifetimeScope().PreserveExistingDefaults();
            //builder.RegisterType<TridionComponentPresentationProvider>().As<IComponentPresentationProvider>().InstancePerLifetimeScope().PreserveExistingDefaults();
            //builder.RegisterType<TridionBinaryProvider>().As<IBinaryProvider>().InstancePerLifetimeScope().PreserveExistingDefaults();
            //builder.RegisterType<TridionLinkProvider>().As<ILinkProvider>().InstancePerLifetimeScope().PreserveExistingDefaults();
            //builder.RegisterType<TridionTaxonomyProvider>().As<ITaxonomyProvider>().InstancePerLifetimeScope().PreserveExistingDefaults();
            //builder.RegisterType<ProvidersFacade>().As<IProvidersFacade>().InstancePerLifetimeScope().PreserveExistingDefaults();

            return builder.Build();
        }
   
    }
}
