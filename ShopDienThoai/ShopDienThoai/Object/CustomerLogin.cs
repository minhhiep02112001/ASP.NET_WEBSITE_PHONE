using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDienThoai.Object
{
    public class CustomerLogin
    {
        [Required]
        public string email { get; set; }
        [MinLength(6)]
        public string password { get; set; }
        public bool Remember { get; set; }
    }
}