//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HisakaWeb.Models
{
    public partial class Brand
    {
        public Brand()
        {
            this.Products = new HashSet<Product>();
        }

        [Required(ErrorMessage = "Brand Name is required")]
        [StringLength(20, ErrorMessage = "Brand Name must be at most 20 characters")]
        public string brandName { get; set; }

        public string brandLogo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
