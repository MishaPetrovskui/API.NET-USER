using Microsoft.AspNetCore.Mvc;
using ShopAPI.Models;
using ShopAPI.Services;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrdersController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public ActionResult<List<Order>> GetOrders()
        {
            return Ok(_orderService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public ActionResult CreateOrder([FromBody] CreateOrderDto dto)
        {
            var (success, error, order) = _orderService.Create(dto);
            if (!success)
                return BadRequest(error);
            return CreatedAtAction(nameof(GetOrder), new { id = order!.Id }, order);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            var order = _orderService.GetById(id);
            if (order == null) return NotFound();
            _orderService.Delete(id);
            return Ok();
        }
    }
}
