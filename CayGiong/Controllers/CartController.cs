using CayGiong.Dao;
using CayGiong.Models;
using CayGiong.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CayGiong.Controllers
{
    public class CartController : BaseController
    {
        private const string mycart = "mycart";
        DaoProduct dao = new DaoProduct();
        // GET: Cart
        public ActionResult Index()
        {
            CartViewModel cartvm = Session[mycart] as CartViewModel;
            if (cartvm != null)
            {
                return View(cartvm);
            }
            else
            {
                return View("_EmptyCart");
            }
        }

        public ActionResult AddCart(int idPro)
        {
            Product product = dao.getDetail(idPro);
            CartViewModel cartvm = Session[mycart] as CartViewModel;
            if (cartvm == null)// chua co san pham
            {
                cartvm = new CartViewModel();
                cartvm.listcart.Add(new CartItemViewModel
                {
                    product = product,
                    Quantity = 1,
                });

            }
            else // da ton tai san pham trong gio hang
            {
                //if (cartvm.listcart.Exists(x => x.product.id_Product == idPro))
                var pro = cartvm.listcart.Where(x => x.product.id_Product == product.id_Product).SingleOrDefault();
                if (pro == null)
                {
                    cartvm.listcart.Add(new CartItemViewModel
                    {
                        product = product,
                        Quantity = 1,
                    });
                }
                else
                {
                    pro.Quantity++;
                }
            }
            //add data for sesion
            Session[mycart] = cartvm;
            return Json(cartvm.listcart.Count.ToString(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CartDetail(int quantity, int idPro)
        {
            Product product = dao.getDetail(idPro);
            CartViewModel cartvm = Session[mycart] as CartViewModel;
            if (cartvm == null)// chua co san pham
            {
                cartvm = new CartViewModel();
                cartvm.listcart.Add(new CartItemViewModel
                {
                    product = product,
                    Quantity = quantity,
                });

            }
            else // da ton tai san pham trong gio hang
            {
                //if (cartvm.listcart.Exists(x => x.product.id_Product == idPro))
                var pro = cartvm.listcart.Where(x => x.product.id_Product == product.id_Product).SingleOrDefault();
                if (pro == null)//k co sp trong list cart
                {
                    cartvm.listcart.Add(new CartItemViewModel
                    {
                        product = product,
                        Quantity = quantity,
                    });
                }
                else
                {
                    pro.Quantity=pro.Quantity+quantity;
                }
            }
            //add data for sesion
            Session[mycart] = cartvm;
            return Json(cartvm.listcart.Count.ToString(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateCart(int idPro)
        {
            CartViewModel cartvm = Session[mycart] as CartViewModel;
            // Product product = dao.getDetail(idPro);
            var cart = cartvm.listcart.Where(x => x.product.id_Product == idPro).SingleOrDefault();
            cart.Quantity++;
            Session[mycart] = cartvm;
            //return Json(cart.Quantity.ToString(), JsonRequestBehavior.AllowGet);
            return PartialView("UpdateCart",cartvm);
        }

        public ActionResult removeCart(int idPro)
        {
            CartViewModel cartvm = Session[mycart] as CartViewModel;
            //Product product = dao.getDetail(idPro);
            var cart = cartvm.listcart.Where(x => x.product.id_Product == idPro).SingleOrDefault();
            if (cart.Quantity == 1)
            {
                cartvm.listcart.Remove(cart);
            }
            else
            {
                cart.Quantity--;
            }
            Session[mycart] = cartvm;
            //return Json(cart.Quantity.ToString(), JsonRequestBehavior.AllowGet);
            return PartialView("UpdateCart", cartvm);
        }

        public ActionResult removeProductCart(int idPro)
        {
            CartViewModel cartvm = Session[mycart] as CartViewModel;
            var cart = cartvm.listcart.Where(x => x.product.id_Product == idPro).SingleOrDefault();
            cartvm.listcart.Remove(cart);
            Session[mycart] = cartvm;
            return PartialView("UpdateCart", cartvm);
        }

        [HttpGet]
        public ActionResult CustomerInfor()
        {
            HttpCookie cookie = Request.Cookies["customer"];
            if (cookie == null)
                return View("CustomerInfor");
            else
            {
                return RedirectToAction("Payment");
            }
        }

        [HttpPost]
        public ActionResult CustomerInfor(CustomerViewModel customerVM)
        {
            HttpCookie cookie = new HttpCookie("customer");
            cookie["name"] = HttpUtility.UrlEncode(customerVM.name);
            cookie["phone"] = customerVM.phone;
            cookie["mail"] = customerVM.mail;
            cookie["address"] =HttpUtility.UrlEncode(customerVM.address);
            cookie.Expires = DateTime.Now.AddMinutes(30);// set time 10 phút

            Response.Cookies.Add(cookie);

            return RedirectToAction("Payment");
        }

        [HttpPost]
        public ActionResult EditCustomerInfor(CustomerViewModel customerVM)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Payment()
        {
            CartViewModel cartvm = Session[mycart] as CartViewModel;
            if (cartvm == null)
                return RedirectToAction("Index", "Home");
            CustomerViewModel customerVM;
            HttpCookie cookie = Request.Cookies["customer"];
            if (cookie != null)
            {
                //var cus = HttpUtility.HtmlDecode(cookie.Value);

                customerVM = new CustomerViewModel()
                {
                    name = HttpUtility.UrlDecode(cookie["name"]),
                    phone = cookie["phone"].ToString(),
                    mail = cookie["mail"].ToString(),
                    address = HttpUtility.UrlDecode(cookie["address"])

                };
                ViewBag.Customer = customerVM;
            }
            var list = cartvm.listcart;
            double total = 0;
            foreach (var item in list)
            {
                total += item.Quantity * (double)item.product.price;
            }
            ViewBag.SumTotal = total;
            ViewBag.CartItem = cartvm.listcart;
            return View();
        }

        [HttpPost]
        public ActionResult Payment(OrderProduct order)
        {
            HttpCookie cookie = Request.Cookies["customer"];
            CartViewModel cartVM = Session[mycart] as CartViewModel;
            decimal? sumtotal = 0;
            foreach (var item in cartVM.listcart)
            {
                sumtotal += (decimal)item.Quantity * item.product.price;
            }
            //add order
            order = new OrderProduct()
            {
                name = HttpUtility.UrlDecode(cookie["name"]),
                email = cookie["mail"].ToString(),
                phone = cookie["phone"].ToString(),
                address = HttpUtility.UrlDecode(cookie["address"]),
                note = order.note,
                total = sumtotal,
                id_Status = 1,
                order_day = DateTime.Now,
            };
            using (var context = new DB_SEED_FARMEntities())
            {
                context.OrderProducts.Add(order);
                context.SaveChanges();
                int? id = context.OrderProducts.OrderByDescending(x => x.id_Order).Take(1).Select(x => x.id_Order).SingleOrDefault();
                //add order detail
                foreach (var item in cartVM.listcart)
                {
                    Order_detal order_Detal = new Order_detal()
                    {
                        id_Order = (int)id,
                        id_Product =item.product.id_Product,
                        quantity=item.Quantity,
                        price=item.product.price,
                    };
                    context.Order_detal.Add(order_Detal);
                    context.SaveChanges();
                    var o = context.OrderProducts.OrderByDescending(x => x.id_Order).Take(1).SingleOrDefault();
                    TempData["time"] = o.order_day;
                    TempData["idOrder"] = o.id_Order;
                    TempData["total"] = string.Format("{0:n0}",o.total);
                }
            };
            return RedirectToAction("AfterPayment");
        }

        public ActionResult AfterPayment()
        {
            CartViewModel cartVM = Session[mycart] as CartViewModel;
            if (cartVM == null)
                return RedirectToAction("Index", "Home");
            Session.Remove(mycart);
            ViewBag.idOrder =TempData["idOrder"];
            ViewBag.Day_Order= TempData["time"];
            ViewBag.total = TempData["total"];
            return View(cartVM);
        }

        public ActionResult BuyNow(int idPro)
        {
            Product product = dao.getDetail(idPro);
            CartViewModel cartvm = Session[mycart] as CartViewModel;
            if (cartvm == null)// chua co san pham
            {
                cartvm = new CartViewModel();
                cartvm.listcart.Add(new CartItemViewModel
                {
                    product = product,
                    Quantity = 1,
                });

            }
            else // da ton tai san pham trong gio hang
            {
                //if (cartvm.listcart.Exists(x => x.product.id_Product == idPro))
                var pro = cartvm.listcart.Where(x => x.product.id_Product == product.id_Product).SingleOrDefault();
                if (pro == null)
                {
                    cartvm.listcart.Add(new CartItemViewModel
                    {
                        product = product,
                        Quantity = 1,
                    });
                }
                else
                {
                    pro.Quantity++;
                }
            }
            //add data for sesion
            Session[mycart] = cartvm;
            return View("CustomerInfor");
        }

        
    }
}