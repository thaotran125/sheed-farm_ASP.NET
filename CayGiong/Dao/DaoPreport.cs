using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CayGiong.Models;

namespace CayGiong.Dao
{
    public class DaoPreport
    {
        public List<pr_RpTurnoverWithMonth_Result> getReportMonth()
        {
            List<pr_RpTurnoverWithMonth_Result> list = new List<pr_RpTurnoverWithMonth_Result>();
            using(var context=new DB_SEED_FARMEntities())
            {
                list = context.pr_RpTurnoverWithMonth().ToList();
                return list;
            }
        }

    }
}