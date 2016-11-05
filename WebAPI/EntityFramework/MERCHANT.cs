namespace WebAPI.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MERCHANT")]
    public partial class MERCHANT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MERCHANT()
        {
            RETRIVAL = new HashSet<RETRIVAL>();
            TRANSACTION_DETAIL = new HashSet<TRANSACTION_DETAIL>();
        }

        [Key]
        [StringLength(10)]
        public string MerchantCode { get; set; }

        [Required]
        [StringLength(50)]
        public string MerchantName { get; set; }

        public int? BackEndProcessor { get; set; }

        [StringLength(1)]
        public string Status { get; set; }

        [StringLength(50)]
        public string Owner { get; set; }

        [StringLength(10)]
        public string MerchantType { get; set; }

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

        [StringLength(50)]
        public string BankCardDBA { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FirstActiveDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LastActiveDate { get; set; }

        [Required]
        [StringLength(10)]
        public string AgentCode { get; set; }

        public virtual AGENT AGENT { get; set; }

        public virtual CITY CITY { get; set; }

        public virtual PROCESSOR PROCESSOR { get; set; }

        public virtual MERCHANT_TYPE MERCHANT_TYPE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RETRIVAL> RETRIVAL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRANSACTION_DETAIL> TRANSACTION_DETAIL { get; set; }
    }
}
