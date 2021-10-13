using CayGiong.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CayGiong.ViewModel
{
    public class TopProductViewModel
    {
        public int id_Product { get; set; }
        public int SumQuantity { get; set; }

        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_detal> Order_detal { get; set; }
    }
}