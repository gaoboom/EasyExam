using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EasyExam.Startup))]
namespace EasyExam
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
