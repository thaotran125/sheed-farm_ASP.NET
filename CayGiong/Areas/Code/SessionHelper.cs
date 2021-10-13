using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CayGiong.Areas.Code
{
    public class SessionHelper
    {
        public static void Setsession(UserSession session)
        {
            HttpContext.Current.Session["LoginSession"] = session;
        }

        public static UserSession GetSession()
        {
            var session = HttpContext.Current.Session["LoginSession"];
            if (session == null)
                return null;
            else
            {
                return session as UserSession;
            }
        }
    }
}