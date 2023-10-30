using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RuntimeCardReader.Core.Implement.Creator
{
    internal class IntrefaceAPICreator
    {

        [DllImport("CRT_310N.dll")]
        public static extern UInt32 CRT310NUOpen();

        [DllImport("CRT_310N.dll")]
        public static extern int CRT310NUClose(UInt32 ComHandle);

        [DllImport("CRT_310N.dll")]
        public static extern int USB_ExeCommand(UInt32 ComHandle, byte TxCmCode, byte TxPmCode, UInt16 TxDataLen, byte[] TxData, ref byte RxReplyType, ref byte RxStCode0, ref byte RxStCode1, ref UInt16 RxDataLen, byte[] RxData);
    }
}
