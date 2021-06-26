namespace ShopDienThoai.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("customer")]
    public partial class customer
    {
        public long id { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        [Column("_address")]
        [StringLength(40)]
        public string C_address { get; set; }

        [StringLength(255)]
        public string pass_word { get; set; }

        public bool? is_active { get; set; }

        public bool? is_remove { get; set; }
    }
}
