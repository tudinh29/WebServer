namespace WebAPI.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MESSAGE")]
    public partial class MESSAGE
    {
        public int ID { get; set; }

        [Required]
        [StringLength(10)]
        public string Sender { get; set; }

        [Required]
        [StringLength(1)]
        public string SenderType { get; set; }

        [StringLength(10)]
        public string Receiver { get; set; }

        [Required]
        [StringLength(1)]
        public string ReceiverType { get; set; }

        [Column("Message", TypeName = "text")]
        public string Message { get; set; }

        public DateTime? DateSend { get; set; }

        public bool? IsRead { get; set; }

        [StringLength(1)]
        public string MessageType { get; set; }
    }
}
