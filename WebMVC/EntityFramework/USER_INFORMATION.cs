namespace WebMVC.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class USER_INFORMATION
    {
        [Key]
        [StringLength(10)]
        public string UserName { get; set; }

        [Required]
        [StringLength(32)]
        public string Password { get; set; }

        [Required]
        [StringLength(1)]
        public string UserType { get; set; }

        [Required]
        [StringLength(1)]
        public string Status { get; set; }
    }
}
