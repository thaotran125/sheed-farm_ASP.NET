using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CayGiong.Models;
using CayGiong.Dao;

namespace CayGiong.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        DaoCategory dao = new DaoCategory();
        // GET: Admin/Category
        public ActionResult Index()
        {
            if (TempData["result"] != null)
                ViewBag.Successmsg = TempData["result"];
            if(TempData["resultEr"]!=null)
                ViewBag.Errormsg = TempData["resultEr"];
            return View(dao.loadCateParent());
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int id)
        {
            Category c = dao.getCateByid(id);
            ViewBag.name = c.name_Cate;
            TempData["group"] = id;
            List<Category> list = new List<Category>();
            list = dao.getCateGroup(id);
            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {

            return PartialView("_AddModal");
        }
        // POST: Admin/Category/Create
        [HttpPost]
        public ActionResult Create(Category category)
        {
            int result;
            result = dao.CreateCate(category);
            if (result == 1)
            {
                TempData["result"] = "Thêm mới thành công";
            }
            else if (result == 0)
            {
                TempData["resultEr"] = "Loại sản phẩm đã tồn tại";
            }
            else
            {
                TempData["resultEr"] = "Vui lòng nhập đủ thông tin!!";
            }
            return RedirectToAction("Index");

        }

        // GET: Admin/Category/Edit/5
        public PartialViewResult Edit(int id)

        {
            Category category = dao.getCateByid(id);
            return PartialView("_EditCate", category);
        }

        // POST: Admin/Category/Edit/5
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            int rs = dao.EditCate(category);
            if (rs == 1)
                TempData["result"] = "Cập nhật thành công";
            else
                TempData["resultEr"] = "Cập nhật thất bại";
            return RedirectToAction("Index", "Category");
        }

        // GET: Admin/Category/Delete/5
        [HttpGet]
        public ActionResult OpenDelete(int id)
        {
            Category model = dao.getCateByid(id);
            return PartialView("_DelModal", model);
        }

        // POST: Admin/Category/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            int rs = dao.DelCate(id);
            if (rs == 1)
                TempData["result"] = "Xóa thành công -.-";
            else
                TempData["resultEr"] = "Đã tồn tại loại sản phẩm...!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult getByid(int idcate)
        {
            Category category = dao.getCateByid(idcate);
            return Json(category, JsonRequestBehavior.AllowGet);
        }
    }
}
