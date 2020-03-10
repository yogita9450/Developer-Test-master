using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zupa.Test.Booking.Models;

namespace Zupa.Test.Booking.Data
{
    public interface IDiscountRepository
    {
        Task<Discount> ReadActiveDiscountAsync();
        Task<Discount> ReadAsync(string coupanCode);
        Task<Discount> SaveAsync(Discount discount);
        Task<Discount[]> SaveDiscountMasterAsync(Discount discount);
    }
}
