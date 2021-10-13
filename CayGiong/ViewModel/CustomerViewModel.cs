using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CayGiong.ViewModel
{
    public class CustomerViewModel
    {
        public string name { get; set; }

        public string phone { get; set; }

        public string mail { get; set; }

        public string address { get; set; }

        public CustomerViewModel() { }

        public CustomerViewModel(string name, string phone, string mail, string address)
        {
            this.name = name;
            this.phone = phone;
            this.mail = mail;
            this.address = address;
        }
    }
}