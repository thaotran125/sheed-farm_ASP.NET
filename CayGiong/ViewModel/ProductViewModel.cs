using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CayGiong.ViewModel
{
    public class ProductViewModel
    {
       // [Display(Name ="id")]
        public int id { get; set; }

      //  [Display(Name = "Tên")]
        public string name { get; set; }

      //  [Display(Name = "Url")]
        public string url { get; set; }

       // [Display(Name = "Hình Ảnh")]
        public string img { get; set; }

       // [Display(Name = "Mô tả")]
        public string title { get; set; }

        //[Display(Name = "Category parent")]
        public int? Group_id { get; set; }
        public string group_name { get; set; }

        //[Display(Name ="Category child")]
        public int id_cate { get; set; }
        public string name_cate { get; set; }


        //[Display(Name = "Khuyến mãi")]
        //public int khuyenmai{ get; set; }

        // [Display(Name = "Giá")]
        public Nullable<decimal> price { get; set; }

        public int? status { get; set; }

        [AllowHtml]
        public string descript { get; set; }
    }
}