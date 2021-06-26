namespace ShopDienThoai.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("banner")]
    public partial class banner
    {
        public long id { get; set; }

        [Required]
        [StringLength(255)]
        public string title { get; set; }

        [Required]
        [StringLength(255)]
        public string slug { get; set; }

        [StringLength(255)]
        public string images { get; set; }

        [StringLength(255)]
        public string url { get; set; }

        [StringLength(255)]
        public string targets { get; set; }

        [Column(TypeName = "ntext")]
        public string descriptions { get; set; }

        public int? position { get; set; }

        public bool? is_active { get; set; }

        public DateTime? create_at { get; set; }

        public bool? is_remove { get; set; }
    }
}
