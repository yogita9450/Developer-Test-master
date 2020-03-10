using System;
using Zupa.Test.Booking.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zupa.Test.Booking.ViewModels
{
    public class Discount
    {
       public string coupanCode { get; set; }
       public DateTime StartDate { get; set; }
       public DateTime ExpirationDate { get; set; }
       public bool IsValid { get; set; }
       public CoupanType Type { get; set; }
       public DiscountType DiscountType { get; set; }
       public double DiscountAmount { get; set; }
       public double DiscountPercentage { get; set; }
    }
}
