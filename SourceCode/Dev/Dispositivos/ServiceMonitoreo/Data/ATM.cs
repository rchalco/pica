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
    
    public partial class ATM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ATM()
        {
            this.Binnacle = new HashSet<Binnacle>();
        }
    
        public int IdATM { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public string Description { get; set; }
        public bool State { get; set; }
        public string Configuration { get; set; }
        public string Profile { get; set; }
        public int IdOffice { get; set; }
        public Nullable<System.DateTime> DateUpdate { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Binnacle> Binnacle { get; set; }
    }
}
