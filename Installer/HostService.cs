using System;
using System.ServiceProcess;
using System.Threading;
using MachSecure.SVM.OverviewService.Nancy;
using MachSecure.SVM.OverviewService.Properties;
using MachSecure.SEMS.BusinessObjects;
using MachSecure.SEMS.DataObjects;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Nancy.Owin;
using Owin;

namespace MachSecure.SVM.OverviewService.Installer
{
    partial class HostService : ServiceBase
    {
        IDisposable host { get; set; }
        public HostService()
        {
            InitializeComponent();
        }

        public void Start()
        {
            var siteUrl = Settings.Default.SiteUrl;
            var portNumber = Settings.Default.PortNumber;
            var uri = $"http://+:{portNumber}{siteUrl}";

         

            StartOptions options = new StartOptions();


#if DEBUG
            options.Urls.Add($"http://{Environment.MachineName}:10000");
            options.Urls.Add("http://+:10000/");
#endif
            options.Urls.Add(uri);
            host = WebApp.Start<Startup>(options);
        }


        protected override void OnStart(string[] args)
        {
            Start();
        }


        protected override void OnStop()
        {
            host.Dispose();
            Thread.Sleep(1500);
        }
    }

    class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app
        .Map("/signalr", map =>
        {

            map.UseCors(CorsOptions.AllowAll);
            var hubConfiguration = new HubConfiguration();
            hubConfiguration.EnableDetailedErrors = true;
            hubConfiguration.EnableJavaScriptProxies = true;
            map.RunSignalR(hubConfiguration);
        })
        .UseNancy();

        }

        private static void Configuration(NancyOptions nancyOptions)
        {
            nancyOptions.Bootstrapper = new Bootstrapper();
        }
    }
}
