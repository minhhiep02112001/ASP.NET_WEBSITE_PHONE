namespace ShopDienThoai.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class article
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

        [Column(TypeName = "ntext")]
        public string summary { get; set; }

        [Column(TypeName = "ntext")]
        public string descriptions { get; set; }

        public bool? is_hot { get; set; }

        [StringLength(255)]
        public string url { get; set; }

        public bool? is_active { get; set; }

        [StringLength(255)]
        public string meta_title { get; set; }

        [StringLength(255)]
        public string meta_description { get; set; }

        public DateTime? created_at { get; set; }

        public bool? is_remove { get; set; }
    }
}
