namespace WebAPI.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("REGION")]
    public partial class REGION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public REGION()
        {
            CITY = new HashSet<CITY>();
        }

        [Key]
        [StringLength(10)]
        public string RegionCode { get; set; }

        [Required]
        [StringLength(50)]
        public string RegionName { get; set; }

        [StringLength(10)]
        public string CountryCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CITY> CITY { get; set; }

        public virtual COUNTRY COUNTRY { get; set; }
    }
}
