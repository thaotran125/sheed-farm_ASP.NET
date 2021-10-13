using CayGiong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CayGiong.ViewModel
{
    public class CartItemViewModel
    {
        //public int idCart { get; set; }

        //// [Display(Name ="id")]
        //public int idPro { get; set; }

        ////  [Display(Name = "Tên")]
        //public string ten { get; set; }

        //// [Display(Name = "Hình Ảnh")]
        //public string img { get; set; }


        //// [Display(Name = "Loại SP")]
        //public int loaiSP_id { get; set; }
        //public string LoaiSP_Ten { get; set; }

        ////[Display(Name = "Khuyến mãi")]
        ////public int khuyenmai{ get; set; }

        //// [Display(Name = "Giá")]
        //public Nullable<decimal> gia { get; set; }

        public Product product { get; set; }

        public int Quantity { get; set; }
    }
}