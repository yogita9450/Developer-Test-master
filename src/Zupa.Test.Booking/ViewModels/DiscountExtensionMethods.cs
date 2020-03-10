using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Models;

namespace Zupa.Test.Booking.ViewModels
{
    public static class DiscountExtensionMethods
    {
        public static Discount ToDiscountViewModel(this Models.Discount discount)
        {
            return new Discount
            {
                coupanCode = discount.coupanCode,
                DiscountAmount = discount.DiscountAmount,
                DiscountPercentage = discount.DiscountPercentage,
                DiscountType = discount.DiscountType,
                ExpirationDate = discount.ExpirationDate,
                IsValid = discount.IsValid,
                StartDate = discount.StartDate,
                Type = discount.Type
            };
        }
      
        public static Models.Discount ToDiscountModel(this Discount discount)
        {
            return new Models.Discount
            {
                coupanCode = discount.coupanCode,
                DiscountAmount = discount.DiscountAmount,
                DiscountPercentage = discount.DiscountPercentage,
                DiscountType = discount.DiscountType,
                ExpirationDate = discount.ExpirationDate,
                IsValid = discount.IsValid,
                StartDate = discount.StartDate,
                Type = discount.Type
            };
        }
    }
}
