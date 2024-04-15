using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServiceFromGPT
{
    [RunInstaller(true)]
    public partial class ServiceInstaller : Installer
    {
        private ServiceProcessInstaller processInstaller;
        private System.ServiceProcess.ServiceInstaller serviceInstaller;

        public ServiceInstaller()
        {
            processInstaller = new ServiceProcessInstaller();
            serviceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 设置服务帐户信息
            processInstaller.Account = ServiceAccount.LocalSystem;
            // 设置服务的安装参数
            serviceInstaller.DisplayName = "Service From GPT4";
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "ServiceFromGPT4";
            this.Installers.Add(processInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            if (System.Environment.UserInteractive)
            {
                string parameter = string.Concat(args);
                switch (parameter)
                {
                    case "--install":
                        ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
                        break;
                    case "--uninstall":
                        ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
                        break;
                }
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new ServiceFromGPT()
                };
                ServiceBase.Run(ServicesToRun);
            }
                
        }
    }
}
