using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zupa.Test.Booking.Data;
using Zupa.Test.Booking.ViewModels;

namespace Zupa.Test.Booking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IBasketsRepository _basketsRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IDiscountRepository _discountRepository;

        public OrdersController(IBasketsRepository basketsRepository, IOrdersRepository ordersRepository, IDiscountRepository discountRepository)
        {
            _basketsRepository = basketsRepository;
            _ordersRepository = ordersRepository;
            _discountRepository = discountRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Order>> PlaceOrder([FromBody]Basket basket)
        {
            //updated order model
            var orderModel = new Models.Order();
            var discountModel = await _discountRepository.ReadActiveDiscountAsync();
            if (discountModel != null)
            {
                orderModel = basket.ToOrderModel(discountModel.ToDiscountViewModel());
            }
            else
            {
                orderModel = basket.ToOrderModel();
            }
            orderModel.Discount = discountModel;
            //orderModel = discountModel.ToOrderModel(orderModel);
            await _ordersRepository.SaveAsync(orderModel);
            await _basketsRepository.ResetBasketAsync();

            var updatedOrder =  CreatedAtAction(
                nameof(GetOrder),
                new { orderModel.ID },
                orderModel.ToOrderViewModel());
           await  _discountRepository.SaveDiscountMasterAsync(discountModel);
            return updatedOrder;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            var order = await _ordersRepository.ReadAsync(id);
            return order.ToOrderViewModel();
        }
    }
}