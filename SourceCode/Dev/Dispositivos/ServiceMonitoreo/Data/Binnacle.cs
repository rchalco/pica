//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceMonitoreo.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Binnacle
    {
        public int IdBinnacle { get; set; }
        public string LevelLog { get; set; }
        public string Description { get; set; }
        public string Trace { get; set; }
        public int IdAtm { get; set; }
        public Nullable<int> IdDevice { get; set; }
        public Nullable<long> IdTransacction { get; set; }
        public string Device { get; set; }
        public string Operation { get; set; }
        public string StateDevice { get; set; }
    
        public virtual ATM ATM { get; set; }
    }
}
