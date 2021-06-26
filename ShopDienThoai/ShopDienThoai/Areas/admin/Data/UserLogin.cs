using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDienThoai.Areas.admin.Data
{
    public class UserLogin
    {
        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [StringLength(255)]
        [MinLength(6)]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}