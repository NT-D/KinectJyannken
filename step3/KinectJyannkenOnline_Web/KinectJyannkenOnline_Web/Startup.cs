using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(KinectJyannkenOnline_Web.Startup))]

namespace KinectJyannkenOnline_Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // アプリケーションの設定方法の詳細については、http://go.microsoft.com/fwlink/?LinkID=316888 を参照してください
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}
