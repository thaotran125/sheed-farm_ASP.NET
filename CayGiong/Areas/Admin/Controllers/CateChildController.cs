using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CayGiong.Models;
using CayGiong.Dao;

namespace CayGiong.Areas.Admin.Controllers
{
    public class CateChildController : BaseController
    {
        DaoCategory dao = new DaoCategory();
        // GET: Admin/CateChild
        public ActionResult Index(int id)
        {
            if (TempData["result"] != null)
            {
                ViewBag.Successmsg = TempData["result"];
            }
            else
                ViewBag.Errormsg = TempData["resultEr"];
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
            return PartialView("_AddCateModal");
        }
        // POST: Admin/Category/Create
        [HttpPost]
        public ActionResult Create(Category category)
        {
            int result, idgroup;
            idgroup = (int)TempData["group"];
            result = dao.CreateCateChild(category, idgroup);
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
            return RedirectToAction("Index", new {id=idgroup });

        }

        // GET: Admin/Category/Edit/5
        public PartialViewResult Edit(int id)
        {
            Category category = dao.getCateByid(id);
            return PartialView("_EditCateModal", category);
        }

        // POST: Admin/Category/Edit/5
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            int idgroup = (int)TempData["group"];
            int rs = dao.EditCate(category);
            if (rs == 1)
                TempData["result"] = "Cập nhật thành công";
            else
                TempData["resultEr"] = "Cập nhật thất bại";
            return RedirectToAction("Index", new { id = idgroup });
        }

        // GET: Admin/Category/Delete/5
        [HttpGet]
        public ActionResult OpenDelete(int id)
        {
            Category model = dao.getCateByid(id);
            return PartialView("_DelCateModal", model);
        }

        // POST: Admin/Category/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            int idgroup = (int)TempData["group"];
            int rs = dao.DelCateChild(id);
            if (rs == 1)
                TempData["result"] = "Xóa thành công -.-";
            else
                TempData["resultEr"] = "Đã tồn tại sản phẩm trong đơn hàng...!";
            return RedirectToAction("Index", new { id = idgroup });
        }
    }
}