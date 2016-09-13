using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CalculadoraJiuJitsu.Startup))]
namespace CalculadoraJiuJitsu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
