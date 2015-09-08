using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using DD4T.RestService.WebApi;

[assembly: OwinStartup(typeof(DD4T.RestService.Startup))]

namespace DD4T.RestService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            log4net.Config.XmlConfigurator.Configure();
            app.UseDD4TWebApi();
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
