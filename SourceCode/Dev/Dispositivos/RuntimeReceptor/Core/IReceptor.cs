using Foundation.Stone.Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace RuntimeReceptor.Core
{
    public interface IReceptor
    {
        string PortCOM { get; set; }
        Response Open(int pMaxAmout);
        Response Close();
        Response Stack();
        Response Eject();
        Response GetState();
    }
}
