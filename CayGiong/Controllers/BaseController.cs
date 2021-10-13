
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
    public class BaseController : Controller
    {
        //DaoCategory dao = new DaoCategory();
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
            CartViewModel cartVm = Session["mycart"] as CartViewModel;
            List<CartItemViewModel> listCart = new List<CartItemViewModel>();
            if (cartVm != null)
            {
                listCart = cartVm.listcart;
                ViewBag.listCart = listCart;
                ViewBag.check = 1;
            }
            else
                ViewBag.check = 0;
        }

    }
}