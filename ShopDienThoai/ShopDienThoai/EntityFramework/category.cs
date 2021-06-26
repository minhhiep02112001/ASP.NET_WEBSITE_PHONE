namespace ShopDienThoai.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("category")]
    public partial class category
    {
        public long id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Required]
        [StringLength(255)]
        public string slug { get; set; }

        [StringLength(255)]
        public string icon { get; set; }

        public long parent_id { get; set; }

        public int position { get; set; }

        public bool? is_active { get; set; }

        public DateTime? create_at { get; set; }

        public bool? is_remove { get; set; }
    }
}
