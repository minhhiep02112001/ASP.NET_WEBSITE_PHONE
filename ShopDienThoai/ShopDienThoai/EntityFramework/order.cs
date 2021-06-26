namespace ShopDienThoai.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class order
    {
        public long id { get; set; }

        public long? customer_id { get; set; }

        [Required]
        [StringLength(255)]
        public string fullname { get; set; }

        [Required]
        [StringLength(255)]
        public string email { get; set; }

        [StringLength(255)]
        public string address_order { get; set; }

        [Required]
        [StringLength(255)]
        public string phone { get; set; }

        [Column(TypeName = "ntext")]
        public string note { get; set; }

        [StringLength(255)]
        public string coupon { get; set; }

        public double? total { get; set; }

        public int? order_Status_id { get; set; }

        public DateTime? create_at { get; set; }

        public bool? is_remove { get; set; }
    }
}
