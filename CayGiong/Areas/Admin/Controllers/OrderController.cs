using CayGiong.Dao;
using CayGiong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace CayGiong.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        DaoOrder dao = new DaoOrder();
        // GET: Admin/Order
        public ActionResult Index(int? page)
        {
            if (TempData["Result"] != null)
                ViewBag.Successmsg = TempData["Result"];
            if (TempData["ResultEr"] != null)
                ViewBag.Errormsg = TempData["ResultEr"];
            int pageNumber = page ?? 1;
            List <OrderProduct> list = dao.loadAddOrder();
            return View(list.ToPagedList(pageNumber,5));
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            OrderProduct o = dao.GetOrderProduct(id);
            ViewBag.NameCus = o.name;
            List<Order_detal> list = dao.getOrderDetail(id);
            return View(list);
        }

        [HttpGet]
        public ActionResult OpendEdit(int id)
        {
            ViewBag.StatusOrder = dao.loadStatusOrder();
            OrderProduct o = dao.GetOrderProduct(id);
            return PartialView("_editstatus", o);
        }

        [HttpPost]
        public ActionResult Edit(OrderProduct o)
        {
            int? page=(int?)TempData["page"];
            int result;
            result= dao.EditOrder(o);
            if (result == 1)
                TempData["Result"]= "Cập nhật đơn hàng thành công";
            else
                TempData["ResultEr"] = "Chưa cập nhật đơn hàng";
            return RedirectToAction("Index",new {page= page });
        }
    }
}