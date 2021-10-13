using CayGiong.Dao;
using CayGiong.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CayGiong.Controllers
{
    public class ProductController : BaseController
    {
        int limitpage = 6;
        DaoProduct dao = new DaoProduct();
        DaoCategory cate = new DaoCategory()
;        // GET: Product
        public ActionResult Index(int? page)
        {
            int pageNuumber = page ?? 1;
            ViewBag.listParent = cate.loadCateParent();
            ViewBag.listGroup = cate.LoadCateGroup();

            List<Product> listProduct = dao.loadAddProduct();
            //ViewBag.SumPage = Math.Ceiling((decimal)dao.loadAddProduct().Count() / limitpage);
            return View(listProduct.ToPagedList(pageNuumber, limitpage));
        }

        public ActionResult ProductParent(int idParent, int? page)
        {
            ViewBag.listParent = cate.loadCateParent();
            ViewBag.listGroup = cate.LoadCateGroup();
            ViewBag.idParent = idParent;
            ViewBag.NameCate = cate.getNameCateParent(idParent);
            int pageNuumber = page ?? 1;
            List<Product> listProduct = cate.loadProductWithIdparent(idParent); ;
            //ViewBag.SumPage = Math.Round((double)cate.loadProductWithIdparent(idParent).Count() / limitpage + 0.45, 0);
            //ViewBag.SumPage = Math.Ceiling((decimal)dao.loadAddProduct().Count() / limitpage);
            return View(listProduct.ToPagedList(pageNuumber, limitpage));
        }

        public ActionResult ProductChild(int idChild,int? page)
        {
            ViewBag.listParent = cate.loadCateParent();
            ViewBag.listGroup = cate.LoadCateGroup();
            ViewBag.idChild = idChild;
            ViewBag.idParent = cate.getIdGroup(idChild);
            ViewBag.NameCate = cate.getNameCateParent(cate.getIdGroup(idChild));
            ViewBag.NameCateChild= cate.getNameCateParent(idChild);
            int pageNuumber = page ?? 1;
            List<Product> listProduct = cate.loadProductWithIdcate(idChild);
            //ViewBag.SumPage = Math.Round((double)cate.loadProductWithIdcate(idChild).Count() / limitpage + 0.45, 0);
            //ViewBag.SumPage = Math.Ceiling((decimal)dao.loadAddProduct().Count() / limitpage);
            return View( listProduct.ToPagedList(pageNuumber, limitpage));
        }

        public ActionResult DetailProduct(int idPro)
        {
            Product product = dao.getDetail(idPro);
            List<Product> result = dao.loadRelateProduct(idPro);
            ViewBag.relateProduct = result;
            ViewBag.Count = result.Count();
            return View(product);
        }

        public ActionResult MinusQuantity(int quantity)
        {

            int sl = 0;
            sl=quantity==1?1:quantity-1;
            return Json(sl.ToString(),JsonRequestBehavior.AllowGet);
        }

        public ActionResult PlusQuantity(int quantity)
        {
            int sl = 0;
            sl=quantity+1;
            return Json(sl.ToString(), JsonRequestBehavior.AllowGet);
        }

    }
}