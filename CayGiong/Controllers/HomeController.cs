using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CayGiong.Models;
using CayGiong.Dao;
using CayGiong.ViewModel;

namespace CayGiong.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        DaoProduct dao = new DaoProduct();
        DaoCategory cate = new DaoCategory();
        int limitpage = 6;
        public ActionResult Index()
        {
            ViewBag.newProduct = dao.loadProdcuctNew();
            ViewBag.topProduct = dao.LoadTopProduct();
            ViewBag.addProduct = dao.loadAddProduct();
            ViewBag.SumPage = dao.loadProduct(limitpage).Count();
            return View();
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            CartViewModel cartVm = Session["mycart"] as CartViewModel;
            //List<CartItemViewModel> listCart = new List<CartItemViewModel>();
            //if (cartVm != null)
            //{
            //    listCart = cartVm.listcart;
            //    ViewBag.listCart = listCart;
            //    ViewBag.check = 1;
            //}
            // else
            //    ViewBag.check = 0;
            ViewBag.QuantityCart = cartVm == null ? 0 : cartVm.listcart.Count();
            ViewBag.listParent = cate.loadCateParent();
            ViewBag.listGroup= cate.LoadCateGroup();
            return PartialView("_Menu");
        }

    }
}