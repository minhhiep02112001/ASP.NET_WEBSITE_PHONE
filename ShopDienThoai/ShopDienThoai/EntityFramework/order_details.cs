namespace ShopDienThoai.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class order_details
    {
        public long id { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(255)]
        public string images { get; set; }

        [StringLength(255)]
        public string sku { get; set; }

        public long order_id { get; set; }

        public long product_id { get; set; }

        public double price { get; set; }

        public int qty { get; set; }

        public double total { get; set; }

        public bool? is_remove { get; set; }
    }
}
