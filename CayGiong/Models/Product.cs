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
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Order_detal = new HashSet<Order_detal>();
        }
    
        public int id_Product { get; set; }
        public string name_product { get; set; }
        public string url_Product { get; set; }
        public string img_Prodcut { get; set; }
        public string title { get; set; }
        public int id_Cate { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<int> status { get; set; }
        public string descript { get; set; }
    
        public virtual Category Category { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_detal> Order_detal { get; set; }
    }
}
