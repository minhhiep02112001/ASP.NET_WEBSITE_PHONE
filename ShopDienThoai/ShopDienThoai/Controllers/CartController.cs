using ShopDienThoai.Common;
using ShopDienThoai.EntityFramework;
using ShopDienThoai.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ShopDienThoai.Controllers
{
    public class CartController : Controller
    {
        public const string CartSession = "CartSession";
        private ShopDienThoaiDbContext db = new ShopDienThoaiDbContext();
        private readonly Random _random = new Random();
        
        /// <summary>
        /// Sử dụng Random 1 string
        /// </summary>    
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
        private string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length=26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
        /// <summary>
        /// End Random
        /// </summary>
        
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var listItem = new List<CartItem>();
            if (cart != null)
            {
                listItem = (List<CartItem>)cart;
            }
            return View(listItem);
        }
        public JsonResult addCart(long id , int quantity)
        {
            var product = db.products.SingleOrDefault(c=>c.id == id);
            if(product != null)
            {
                if(quantity > product.quantity)
                {
                    return Json(new
                    {
                        status = "false",
                        text = "Số lượng không đủ bán"
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (this.changeCart(id, quantity))
                    {
                        return Json(new
                        {
                            status = "true"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            status = "false",
                            text = "Số lượng không đủ bán"
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                return Json(new
                {
                    status = "false",
                    text = "Sản phẩm không tồn tại"
                }, JsonRequestBehavior.AllowGet) ;
            }
            
        }
        
        [HttpPost]
        public ActionResult DatHang(FormCollection formCollection)
        {
            var cart = Session[CartSession];
            var listItem = new List<CartItem>();
            if (cart != null)
            {
                listItem = (List<CartItem>)cart;
            }
            else
            {
                Session["Status"] = "Chưa tồn tại giỏ hàng";
                return RedirectToRoute("Cart" , new { action = "Index" });
            }

            var name = formCollection["name"];
            var address = formCollection["address"];
            var phone = formCollection["phone"];
            var email = formCollection["email"];
            var ghichu = formCollection["ghiChu"];
            var order = new order();
            order.create_at = DateTime.Now;

            /// random mã hóa đơn
            var mahd = new StringBuilder();
            // 4-Letters lower case   
            mahd.Append(RandomString(6, true));
            // 4-Digits between 1000 and 9999  
            mahd.Append(RandomNumber(1000, 9999));
            order.coupon = "HD_" + mahd;


            order.email = email;
            var sessionCustomer = (CustomerSession)Session["CUSTOMER_SESSION"];
            order.customer_id = sessionCustomer != null ? sessionCustomer.id : 0 ;
            order.note = ghichu;
            order.phone = phone;
            order.total = listItem.Sum(c => c.total);
            order.order_Status_id = 0;
            order.is_remove = false;
            order.fullname = name;
            order.address_order = address;
            var order_add = db.orders.Add(order);
            db.SaveChanges();
            foreach(var item in listItem)
            {
                var order_detail = new order_details();
                order_detail.images = item.images;
                order_detail.order_id = order_add.id;
                order_detail.price = item.price;
                order_detail.qty = item.quatity;
                order_detail.name = item.ten_sp;
                
                order_detail.product_id = item.product_id;
                order_detail.total = item.total;
                order_detail.is_remove = false;
                db.order_details.Add(order_detail);
                db.SaveChanges();
            }
            Session.Remove(CartSession);
            Session["Status"] = "Đặt hàng thành công";
            return RedirectToRoute("Cart", new {action ="Index" });
        }
        public bool changeCart(long id, int quantity)
        {
            var cart = Session[CartSession];
            var listItem = new List<CartItem>();
            
            if (cart != null)
            {
                listItem = (List<CartItem>)cart;
                var product = db.products.Find(id);
                var itemCart = listItem.SingleOrDefault(x => x.product_id == id);
                if(itemCart!= null)
                {
                    var sum = itemCart.quatity + quantity;
                    if(sum > product.quantity)
                    {
                        return false;
                    }
                }

                if (itemCart != null)
                {
                    if (quantity == 0)
                    {
                        var cartItem = listItem.SingleOrDefault(c => c.product_id == id);
                        listItem.Remove(cartItem);
                    }
                    else
                    {
                        foreach (var item in listItem)
                        {
                            if (item.product_id == id)
                            {
                                item.quatity += quantity;
                                item.total = item.quatity * item.price;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    
                    var item = new CartItem();
                    item.product_id = id;
                    item.ten_sp = product.name;
                    item.slug = product.slug;
                    item.quatity = quantity;
                    item.images = product.images;
                    item.price = product.price;
                    item.total = (float)product.sale * quantity;
                    listItem.Add(item);
                }
                Session[CartSession] = listItem;
            }
            else
            {
                var product = db.products.Find(id);
                var item = new CartItem();
                item.product_id = id;
                item.ten_sp = product.name;
                item.slug = product.slug;
                item.quatity = quantity;
                item.images = product.images;
                item.price = product.price;
                item.total = (product.sale * quantity);
                listItem.Add(item);
                Session[CartSession] = listItem;
            }
            return true;
        }

        public JsonResult chageQuantityCart(long id, int quantity)
        {
            var cart = Session[CartSession];
            var listItem = new List<CartItem>();
            if (cart == null)
            {
                return Json(new
                {
                    status = "false",
                    text = "Sản phẩm không tồn tại"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                listItem = (List<CartItem>)cart;
                var product = db.products.SingleOrDefault(c => c.id == id);
                if (quantity == 0)
                {
                    var cartItem = listItem.SingleOrDefault(c => c.product_id == id);
                    listItem.Remove(cartItem);
                    Session[CartSession] = listItem;
                    return Json(new
                    {
                        status = "true",

                    }, JsonRequestBehavior.AllowGet);
                }
                if (product != null)
                {
                    if (quantity > product.quantity)
                    {
                        return Json(new
                        {
                            status = "false",
                            text = "Số lượng không đủ bán"
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (listItem.Exists(c => c.product_id == id))
                        {
                            foreach (var item in listItem)
                            {
                                if (item.product_id == id)
                                {
                                    item.quatity = quantity;
                                    item.total = quantity * item.price;
                                    break;
                                }
                            }
                            Session[CartSession] = listItem;
                            return Json(new
                            {
                                status = "true"
                            }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            return Json(new
                            {
                                status = "true",

                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
                else
                {
                    return Json(new
                    {
                        status = "false",
                        text = "Sản phẩm không tồn tại"
                    }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}