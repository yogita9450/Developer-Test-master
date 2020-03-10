using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Zupa.Test.Booking.Controllers;
using Zupa.Test.Booking.Data;
using Zupa.Test.Booking.Models;
using Zupa.Test.Booking.ViewModels;

namespace Zupa.Booking.Tests
{
   
    public class DiscountControllerTests
    {
        IDiscountRepository _discountRepo;
        DiscountController _discountController;
        public DiscountControllerTests()
        {
            _discountRepo = new InMemoryDiscountRepositoryFake();
            _discountController = new DiscountController(_discountRepo);


        }
        [Fact]
        public void GetDiscountCoupan_WhenCalled_ReturnsOkResult()
        {
            var okResult = _discountController.GetDiscountCoupan("Discount20");
            Assert.IsType<Test.Booking.ViewModels.Discount>(okResult.Result.Value);
        }
    }
   
}
