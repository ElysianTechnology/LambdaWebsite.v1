using System;
using System.IO;
using System.Reflection;
using System.ServiceProcess;
using FileLogging4Net;
using MachSecure.SVM.OverviewService.Installer;
using MachSecure.SVM.OverviewService.Properties;

namespace MachSecure.SVM.OverviewService
{
    class Program
    {
        internal static TextFileLogger ProcessLog;
        static void Main(string[] args)
        {
           
#if DEBUG
            var service = new HostService();
            service.Start();
            Console.WriteLine("Service Running...");
            Console.ReadLine();
#else
            RunService();
#endif
        }
        static void RunService()
        {
            var ServicesToRun = new ServiceBase[]
            {
                new HostService()
            };
            ServiceBase.Run(ServicesToRun);
        }
        internal static string Title
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length <= 0)
                    return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
                var titleAttribute = (AssemblyTitleAttribute)attributes[0];
                return titleAttribute.Title.Length > 0 ? titleAttribute.Title : Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }
        internal static Version Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        internal static string AssemblyName
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Name;
            }
        }
    }
}
