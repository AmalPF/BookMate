//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookMate.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Books
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Books()
        {
            this.Cart = new HashSet<Cart>();
            this.Purchase = new HashSet<Purchase>();
            this.Rating = new HashSet<Rating>();
        }
        public int BId { get; set; }

        [Required]
        [Display(Name = "Book Name")]
        public string BName { get; set; }

        [Required]
        [Display(Name = "Author Name")]
        public string BAuthor { get; set; }

        [Required]
        [Display(Name = "Publisher Name")]
        public string BPublisher { get; set; }

        [Required]
        [Display(Name = "Year of Publishing")]
        public int BYearOfPublication { get; set; }

        [Required]
        [Display(Name = "Book Category")]
        public string BCategory { get; set; }

        [Required]
        [Display(Name = "Book Image")]
        public string BImage { get; set; }

        [Required]
        [Display(Name = "Book Price")]
        public double BPrice { get; set; }

        [Required]
        [Display(Name = "Stock Quantity")]
        public int BQuantity { get; set; }

        [Display(Name = "Number of Purchases")]
        public int BNPurchases { get; set; }

        [Display(Name = "Book Rating")]
        public Nullable<double> BRating { get; set; }
        

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Cart { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Purchase> Purchase { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rating> Rating { get; set; }
    }
}
