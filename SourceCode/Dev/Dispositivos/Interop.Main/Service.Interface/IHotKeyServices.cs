using Foundation.Stone.Application.Module;
using Foundation.Stone.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interop.Main.Service.Interface
{
    //[ModuleStone(FullName = "ATM.ControlPanel.Services.HotKeyServices")]
    public interface IHotKeyServices
    {
        Response InitHotKey();
        Response ShowFormMain();
    }
}
