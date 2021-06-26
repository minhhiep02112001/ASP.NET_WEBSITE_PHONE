namespace ShopDienThoai.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("comment")]
    public partial class comment
    {
        public long id { get; set; }

        public long? articles_id { get; set; }

        public long? customer_id { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(255)]
        public string meta_title { get; set; }

        public DateTime? created_at { get; set; }

        public bool? is_remove { get; set; }
    }
}
