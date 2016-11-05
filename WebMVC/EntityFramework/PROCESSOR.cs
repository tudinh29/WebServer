namespace WebMVC.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PROCESSOR")]
    public partial class PROCESSOR
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PROCESSOR()
        {
            MERCHANT = new HashSet<MERCHANT>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string ProcessorCode { get; set; }

        [Required]
        [StringLength(50)]
        public string ProcessorName { get; set; }

        [StringLength(1)]
        public string Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ApprovalDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CloseDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FirstActiveDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastActiveDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MERCHANT> MERCHANT { get; set; }
    }
}
