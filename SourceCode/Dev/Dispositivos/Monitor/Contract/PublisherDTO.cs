using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Monitoreo.Contract
{

    public class PublisherDTO
    {

        public int IdPublisher { get; set; }

        public string Name { get; set; }

        public string IP { get; set; }

        public string Identificador { get; set; }

        public string Detail { get; set; }
    }
}
