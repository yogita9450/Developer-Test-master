using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zupa.Test.Booking.Data;
using Zupa.Test.Booking.Models;

namespace Zupa.Booking.Tests
{
    public class InMemoryDiscountRepositoryFake : IDiscountRepository
    {
        private List<Discount> _DiscountRepository;
        private Discount _activeDiscount;
        public InMemoryDiscountRepositoryFake()
        {
            _DiscountRepository = new List<Discount> {
                new Discount  {coupanCode="Discount20", DiscountPercentage=20, StartDate = new DateTime(2018,1,1), ExpirationDate = new DateTime(2020,4,30), DiscountType = DiscountType.Percentage, IsValid= true },
                new Discount  {coupanCode="Discount40", DiscountPercentage=40, StartDate = new DateTime(2018,1,1), ExpirationDate = new DateTime(2020,4,30), DiscountType = DiscountType.Percentage, IsValid= true },
                new Discount  {coupanCode="Discount10", DiscountPercentage=10, StartDate = new DateTime(2018,1,1), ExpirationDate = new DateTime(2020,4,30), DiscountType = DiscountType.Percentage, IsValid= true },
                new Discount  {coupanCode="Discount50", DiscountPercentage=50, StartDate = new DateTime(2018,1,1), ExpirationDate = new DateTime(2020,4,30), DiscountType = DiscountType.Percentage, IsValid= true },
            };
            _activeDiscount = new Discount();
        }
        public Task<Discount> ReadActiveDiscountAsync()
        {
            return Task.FromResult(_activeDiscount);
        }

        public Task<Discount> ReadAsync(string coupanCode)
        {
            return Task.FromResult(_DiscountRepository.FirstOrDefault(Discount => Discount.coupanCode == coupanCode && Discount.IsValid == true));
        }

        public Task<Discount> SaveAsync(Discount discount)
        {
            _activeDiscount = discount;
            return Task.FromResult(_activeDiscount);
        }

        public Task<Discount[]> SaveDiscountMasterAsync(Discount discount)
        {
            _DiscountRepository.Where(x => x.coupanCode == discount.coupanCode).ToList().ForEach(c => c.IsValid = false);
            _activeDiscount = new Discount();
            return Task.FromResult(_DiscountRepository.ToArray());
        }
    }
}
