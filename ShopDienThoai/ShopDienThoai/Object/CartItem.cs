using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopDienThoai.Object
{
    public class CartItem
    {
        public long product_id { get; set; }
        public string ten_sp { get; set; }
        public string slug { get; set; }
        public string images { get; set; }
        public int quatity { get; set; }
        public int price { get; set; }
        public float total { get; set; }
    }
}