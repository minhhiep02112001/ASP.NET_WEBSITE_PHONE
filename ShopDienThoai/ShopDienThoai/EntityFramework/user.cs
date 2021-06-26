namespace ShopDienThoai.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user
    {
        public long id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Required]
        [StringLength(255)]
        public string email { get; set; }

        [StringLength(255)]
        public string passwords { get; set; }

        [StringLength(100)]
        public string remember_token { get; set; }

        public DateTime? created_at { get; set; }

        [StringLength(255)]
        public string avatar { get; set; }

        public bool? is_active { get; set; }

        public bool? is_remove { get; set; }
    }
}
