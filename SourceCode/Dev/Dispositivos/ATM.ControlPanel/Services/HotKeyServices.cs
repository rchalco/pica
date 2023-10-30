using ATM.ControlPanel.Core;
using Foundation.Stone.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.ControlPanel.Services
{
    internal class HotKeyServices : IHotKeyServices, Interop.Main.Service.Interface.IHotKeyServices
    {
        [STAThread]
        public Response InitHotKey()
        {
            DeamonHotKey deamonHotKey = new DeamonHotKey();
            return new Response { State = ResponseType.Success, Message = "HotKye inicializado" };
        }

        [STAThread]
        public Response ShowFormMain()
        {
            MainControlPanel mainControlPanel = new MainControlPanel();
            mainControlPanel.Show();
            return new Response { State = ResponseType.Success, Message = "ShowFormMain inicializado" };

        }
    }
}
