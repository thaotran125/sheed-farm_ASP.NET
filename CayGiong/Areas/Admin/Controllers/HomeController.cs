using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CayGiong.Models;
using CayGiong.Dao;

namespace CayGiong.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        DaoPreport dao = new DaoPreport();
        // GET: Admin/Home
        public ActionResult Index()
        {
            List<pr_RpTurnoverWithMonth_Result> listReport = new List<pr_RpTurnoverWithMonth_Result>();
            List<int> listMonth = new List<int>();
            IList<double> listTurnover = new List<double>();
            using(var context=new DB_SEED_FARMEntities())
            {
                listReport= context.pr_RpTurnoverWithMonth().ToList();
                ViewBag.pay=context.OrderProducts.Where(x=>x.id_Status==1).Count();
                ViewBag.ship = context.OrderProducts.Where(x => x.id_Status == 2).Count();
                ViewBag.receive = context.OrderProducts.Where(x => x.id_Status == 3).Count();
                ViewBag.cancel = context.OrderProducts.Where(x => x.id_Status == 4).Count();

            }
            foreach(var item in listReport)
            {
                listMonth.Add((int)item.month);
                listTurnover.Add((double)item.sumtotal);
            }
            ViewBag.month = listMonth.Distinct();
            ViewBag.sumTotal = listTurnover.ToList();

            return View();
        }

        public ActionResult DataFromDataBase()
        {
            JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

            try
            {
                using(var context = new DB_SEED_FARMEntities())
                {
                    ViewBag.DataPoints = JsonConvert.SerializeObject(context.pr_RpTurnoverWithDay(), _jsonSetting);
                    return View();
                }
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                return View("Error");
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return View("Error");
            }
        }
    }
}