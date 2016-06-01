using System.ComponentModel;
using MachSecure.SVM.OverviewService.Properties;

namespace MachSecure.SVM.OverviewService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();

            HostServiceInstaller.DisplayName = Settings.Default.DisplayName;
            HostServiceInstaller.Description = Settings.Default.Description;
            HostServiceInstaller.ServiceName = Settings.Default.ServiceName;
        }
    }
}
