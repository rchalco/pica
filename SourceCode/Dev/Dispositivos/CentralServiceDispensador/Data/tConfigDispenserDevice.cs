//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CentralServiceDispensador.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tConfigDispenserDevice
    {
        public int IdTypeDevice { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> Version { get; set; }
        public string XmlGeneralConfig { get; set; }
    
        public virtual tTypeDevice tTypeDevice { get; set; }
    }
}
