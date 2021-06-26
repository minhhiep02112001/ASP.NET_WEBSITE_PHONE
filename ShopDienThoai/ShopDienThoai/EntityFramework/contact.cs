namespace ShopDienThoai.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("contact")]
    public partial class contact
    {
        public long id { get; set; }

        [StringLength(255)]
        public string name { get; set; }

        [StringLength(255)]
        public string email { get; set; }

        [StringLength(255)]
        public string phone { get; set; }

        [StringLength(255)]
        public string content { get; set; }

        public DateTime? create_at { get; set; }
    }
}
