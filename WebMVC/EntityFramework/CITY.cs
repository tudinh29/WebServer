namespace WebMVC.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CITY")]
    public partial class CITY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CITY()
        {
            AGENT = new HashSet<AGENT>();
            MERCHANT = new HashSet<MERCHANT>();
        }

        [Key]
        [StringLength(10)]
        public string CityCode { get; set; }

        [Required]
        [StringLength(50)]
        public string CityName { get; set; }

        [StringLength(10)]
        public string RegionCode { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AGENT> AGENT { get; set; }

        public virtual REGION REGION { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MERCHANT> MERCHANT { get; set; }
    }
}
