//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DBWorker
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rental
    {
        public int RentalID { get; set; }
        public System.DateTime PickUpDate { get; set; }
        public Nullable<System.DateTime> DropOffDate { get; set; }
        public int CustomerID { get; set; }
        public int CarID { get; set; }
        public string Note { get; set; }
    
        public virtual Car Car { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
