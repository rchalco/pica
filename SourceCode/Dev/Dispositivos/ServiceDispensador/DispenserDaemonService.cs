using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDispensador
{
    [RunInstaller(true)]
    public partial class DispenserDaemonService : System.Configuration.Install.Installer
    {
        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
        private System.ServiceProcess.ServiceInstaller FingerprintWinServiceInstaller = new System.ServiceProcess.ServiceInstaller();
        public DispenserDaemonService()
        {
            InitializeComponent();
            this.serviceProcessInstaller1.Password = null;
            this.serviceProcessInstaller1.Username = null;

            this.FingerprintWinServiceInstaller.Description = "Servicio Dispensador";
            this.FingerprintWinServiceInstaller.DisplayName = "Dispensador";
            this.FingerprintWinServiceInstaller.ServiceName = "DispensadorService";

            this.Installers.AddRange(new System.Configuration.Install.Installer[] { this.FingerprintWinServiceInstaller, this.serviceProcessInstaller1 });
        }
    }
}
