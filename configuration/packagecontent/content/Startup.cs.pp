using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using DD4T.RestService.WebApi;

[assembly: OwinStartup(typeof($rootnamespace$.Startup))]

namespace $rootnamespace$
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            log4net.Config.XmlConfigurator.Configure();

            //in case of additional mappings are needed for your specific implementation;
            //Create a AutoFac ContainerBuilder and add your specific binding before calling th app.UseDD4TWebApi();
            //var container = new ContainerBuilder();

            //container.RegisterType<MyClass>()
            //    .As<IMyInterface>()
            //    .InstancePerLifetimeScope();

            //app.UseDD4TWebApi(container);

            app.UseDD4TWebApi();
        }
    }
}
