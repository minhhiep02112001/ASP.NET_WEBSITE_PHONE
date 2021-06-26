namespace ShopDienThoai.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("product")]
    public partial class product
    {
        public long id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Required]
        [StringLength(255)]
        public string slug { get; set; }

        [StringLength(255)]
        public string images { get; set; }

        public int price { get; set; }

        public int sale { get; set; }

        public int quantity { get; set; }

        public bool? is_active { get; set; }

        public bool? is_hot { get; set; }

        public long category_id { get; set; }

        [StringLength(255)]
        public string url { get; set; }

        [StringLength(255)]
        public string sku { get; set; }

        [StringLength(255)]
        public string color { get; set; }

        [StringLength(255)]
        public string memory { get; set; }

        [Column(TypeName = "ntext")]
        public string summary { get; set; }

        [Column(TypeName = "ntext")]
        public string descriptions { get; set; }

        [StringLength(255)]
        public string meta_title { get; set; }

        [Column(TypeName = "ntext")]
        public string meta_description { get; set; }

        public DateTime? create_at { get; set; }

        public bool? is_remove { get; set; }
    }
}
