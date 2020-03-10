using System;
using System.Linq;

namespace Zupa.Test.Booking.ViewModels
{
    public static class BasketExtensionMethods
    {
        public static Models.Order ToOrderModel(this Basket basket, Discount discount)
        {
            return new Models.Order
            {
                ID = Guid.NewGuid(),
                GrossTotal = basket.Items.Sum(item => item.GrossPrice),
                NetTotal = basket.Items.Sum(item => (item.NetPrice*(1-(discount.DiscountPercentage/100)))),
                TaxTotal = basket.Items.Sum(item => (item.NetPrice * (1 -( discount.DiscountPercentage / 100))) * item.TaxRate),
                Items = basket.Items.ToOrderItemModels()
            };
        }

        public static Basket ToBasketViewModel(this Models.Basket basket)
        {
            return new Basket
            {
                Items = basket.Items.ToBasketItemViewModels()
            };
        }

        public static Models.Order ToOrderModel(this Basket basket)
        {
            return new Models.Order
            {
                ID = Guid.NewGuid(),
                GrossTotal = basket.Items.Sum(item => item.GrossPrice),
                NetTotal = basket.Items.Sum(item => (item.NetPrice)),
                TaxTotal = basket.Items.Sum(item => (item.NetPrice * item.TaxRate)),
                Items = basket.Items.ToOrderItemModels()
            };
        }

    }
}
