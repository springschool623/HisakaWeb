//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HisakaWeb.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderDetail
    {
        public int orderDetailID { get; set; }
        public string productID { get; set; }
        public Nullable<int> orderQuantity { get; set; }
        public string orderID { get; set; }
    
        public virtual ProductOrder ProductOrder { get; set; }
        public virtual Product Product { get; set; }
    }
}
