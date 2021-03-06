//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GESIDENTITY.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Federations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Federations()
        {
            this.Sections = new HashSet<Sections>();
            this.Identifications = new HashSet<Identifications>();
        }
    
        public int ID { get; set; }
        public Nullable<int> idDepartement { get; set; }
        public Nullable<int> idRegion { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Etat { get; set; }
    
        public virtual Departements Departements { get; set; }
        public virtual Regions Regions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sections> Sections { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Identifications> Identifications { get; set; }
    }
}
