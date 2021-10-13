using CayGiong.Dao;
using CayGiong.Models;
using CayGiong.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace CayGiong.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        DaoProduct dao = new DaoProduct();
        DaoCategory cate = new DaoCategory();

        // GET: Admin/Product
        public ActionResult Index(int? page, int? PageSize)
        {
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="30", Text= "30" },
            };
            int pageNumber = page ?? 1;
            int pagesize = PageSize ?? 5;

            List<Product> list = dao.PagelistAdmin();
            if (TempData["result"] != null)
                ViewBag.Successmsg = TempData["result"];
            if (TempData["resultEr"] != null)
                ViewBag.Errormsg = TempData["resultEr"];
            return View(list.ToPagedList(pageNumber, pagesize));
        }

        public ActionResult Detail(int id)
        {
            ProductViewModel p = dao.DetailProductVM(id);
            return View(p);
        }

        [HttpGet]
        public ActionResult Insert()
        {
            ViewBag.CateParent = cate.loadCateParent();
            List<Product> list = new List<Product>();
            ViewBag.Category = list;
            return View();
        }

        [HttpPost]
        public ActionResult getCateChild(int id)
        {
            //if (!string.IsNullOrWhiteSpace(id.ToString()))
            //{
            //    IEnumerable<SelectListItem> cateChild = dao.getCateChild(id);
            //    return Json(cateChild, JsonRequestBehavior.AllowGet);
            //}
            //return null;

            ViewBag.Category = cate.getCateGroup(id);
            return PartialView("DisplayCateChild");
        }

        [HttpPost]
        public ActionResult Insert(ProductViewModel productvm)
        {
            int result = dao.Insert_Product(productvm);
            if (result == 1)
                TempData["result"] = "Thêm sản phẩm mới thành công";
            else
                TempData["resultEr"] = "Đã tồn tại tên sản phẩm";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.CateParent = cate.loadCateParent();
            List<Category> list = new List<Category>();
            ProductViewModel p = dao.DetailProductVM(id);
            list = cate.getCateGroup(p.Group_id);
            ViewBag.Category = list;
            return View(p);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel productvm)
        {
            int? page, size;
            page = (int?)TempData["page"]; size = (int?)TempData["pagesize"];
            int result = dao.Edit_Product(productvm);
            if (result == 1)
                TempData["result"] = "Cập nhật thành công";
            else
                TempData["resultEr"] = "Cập nhật thất bại";
            return RedirectToAction("Index", new { page = page, PageSize = size });
        }

        public ActionResult Delete(int id)
        {
            dao.del_Product(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Search(string textSearch, int? page, int? PageSize)
        {
            List<Product> result = new List<Product>();
            TempData["text"] = textSearch;
            result = dao.search_Product(textSearch);
            if (result.Count() > 0)
            {
                ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="30", Text= "30" },
            };
                int pageNumber = page ?? 1;
                int pagesize = PageSize ?? 5;
                return View("Search", result.ToPagedList(pageNumber, pagesize));
            }
            else
            {
                ViewBag.searchEr = "Không tìm thấy kết quả tìm kiếm của '" + textSearch + "'";
                return View();
            }
        }

        [HttpGet]
        public ActionResult EditSearch(string textSearch, int id)
        {
            TempData["text"] = textSearch;
            ViewBag.CateParent = cate.loadCateParent();
            List<Category> list = new List<Category>();
            ProductViewModel p = dao.DetailProductVM(id);
            list = cate.getCateGroup(p.Group_id);
            ViewBag.Category = list;
            return View(p);
        }

        [HttpPost]
        public ActionResult EditSearch(ProductViewModel productvm)
        {
            int? page, size;
            string text = TempData["text"].ToString();
            page = (int?)TempData["page"]; size = (int?)TempData["pagesize"];
            int result = dao.Edit_Product(productvm);
            if (result == 1)
                TempData["result"] = "Cập nhật thành công";
            else
                TempData["resultEr"] = "Cập nhật thất bại";
            return RedirectToAction("Search", new { textSearch= text,page = page, PageSize = size });
        }
    }
}