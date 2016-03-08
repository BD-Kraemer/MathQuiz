using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MathQuiz.Startup))]
namespace MathQuiz
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
