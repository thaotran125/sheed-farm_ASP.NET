//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CayGiong.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrderProduct()
        {
            this.Order_detal = new HashSet<Order_detal>();
        }
    
        public int id_Order { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string note { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<int> id_Status { get; set; }
        public Nullable<System.DateTime> order_day { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_detal> Order_detal { get; set; }
        public virtual Status_Order Status_Order { get; set; }
    }
}
