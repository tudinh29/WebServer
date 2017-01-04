namespace WebMVC.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CARD")]
    public partial class CARD
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CARD()
        {
            RETRIVAL = new HashSet<RETRIVAL>();
            TRANSACTION_DETAIL = new HashSet<TRANSACTION_DETAIL>();
        }

        [Key]
        public Byte[] AccountNumber { get; set; }

        [StringLength(50)]
        public string FullName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpirationDate { get; set; }

        [StringLength(12)]
        public string FirstTwelveAccountNumber { get; set; }

        [Required]
        [StringLength(10)]
        public string CardTypeCode { get; set; }

        public virtual CARD_TYPE CARD_TYPE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RETRIVAL> RETRIVAL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRANSACTION_DETAIL> TRANSACTION_DETAIL { get; set; }
    }
}
