using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CayGiong.Areas.Code
{
    [Serializable]
    public class UserSession
    {
        public int idNhanVien { get; set; }
        public string userName { get; set; }
    }
}