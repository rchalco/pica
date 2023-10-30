using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneradorLotes
{
    public class Lote
    {
        public int nro { get; set; }
        public long pan_inicial { get; set; }
        public long pan_final { get; set; }
        public int cantidad { get; set; }
        public string fecha_vencimiento { get; set; }
    }

    public class LoteDistribucion
    {
        public string codigoSucursal { get; set; }
        public int cantidad { get; set; }
    }
}
