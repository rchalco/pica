using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ServiceMonitoreo.Contract
{
    [DataContract]
    [Serializable]
    public class ResulCommandCardReader
    {
        [DataMember]
        public string NameCommand { get; set; }
        [DataMember]
        public string Cm { get; set; }
        [DataMember]
        public string Pm { get; set; }
        [DataMember]
        public int TxDataLen { get; set; }
        [DataMember]
        public int RxDataLen { get; set; }
        [DataMember]
        public string TxData { get; set; }
        [DataMember]
        public string RxData { get; set; }
        [DataMember]
        public string ReType { get; set; }
        [DataMember]
        public string St0 { get; set; }
        [DataMember]
        public string St1 { get; set; }

        public void convertB64Data()
        {
            if (!string.IsNullOrEmpty(RxData))
            {
                byte[] bytes = Convert.FromBase64String(RxData);
                RxData = BitConverter.ToString(bytes);
            }

            if (!string.IsNullOrEmpty(TxData))
            {
                byte[] bytes = Convert.FromBase64String(TxData);
                TxData = BitConverter.ToString(bytes);
            }
        }
    }
}
