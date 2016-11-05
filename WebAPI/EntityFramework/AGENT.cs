namespace WebAPI.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AGENT")]
    public partial class AGENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AGENT()
        {
            MERCHANT = new HashSet<MERCHANT>();
        }

        [Key]
        [StringLength(10)]
        public string AgentCode { get; set; }

        [Required]
        [StringLength(50)]
        public string AgentName { get; set; }

        [StringLength(1)]
        public string AgentStatus { get; set; }

        [StringLength(50)]
        public string Owner { get; set; }

        [StringLength(50)]
        public string Address1 { get; set; }

        [StringLength(50)]
        public string Address2 { get; set; }

        [StringLength(50)]
        public string Address3 { get; set; }

        [StringLength(10)]
        public string CityCode { get; set; }

        public int? Zip { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ApprovalDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CloseDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FirstActiveDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastActiveDate { get; set; }

        public virtual CITY CITY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MERCHANT> MERCHANT { get; set; }
    }
}
