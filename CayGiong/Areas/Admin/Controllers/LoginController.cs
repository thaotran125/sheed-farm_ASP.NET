using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CayGiong.Areas.Code;
using CayGiong.Dao;
using CayGiong.Models;

namespace CayGiong.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        DaoLogin dao = new DaoLogin();
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Account user)
        {
            if (ModelState.IsValid)
            {
                int result = dao.Login(user.userName, user.password);
                if (result == 1)
                {
                    var userSession = new UserSession();
                    var acc = dao.getUserByid(user.userName);
                    userSession.userName = acc.userName;
                    userSession.idNhanVien = acc.id_employee;
                    Session.Add(CommonContants.user_session, userSession);
                    // SessionHelper.Setsession(new UserSession() { userName = user.userName, idNhanVien = user.nhanvien_id });
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                else if (result == -1)
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
            }
            else
                ModelState.AddModelError("", "vui lòng nhập đủ thông tin");
            return RedirectToAction("Index","Home");
        }
    }
}