using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CayGiong.Models;

namespace CayGiong.ViewModel
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            listcart = new List<CartItemViewModel>();
        }

        public List<CartItemViewModel> listcart;
        public int id { get; set; }
    }
}