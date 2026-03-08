using Microsoft.EntityFrameworkCore;
using ShopAPI.Models;

namespace ShopAPI.Services
{
    public class OrderService
    {
        private readonly ShopDbContext _context;

        public OrderService(ShopDbContext context)
        {
            _context = context;
        }

        public List<Order> GetAll() =>
            _context.Orders
                .Include(o => o.ProductsInOrders)
                    .ThenInclude(p => p.Product)
                .ToList();

        public Order? GetById(int id) =>
            _context.Orders
                .Include(o => o.ProductsInOrders)
                    .ThenInclude(p => p.Product)
                .FirstOrDefault(o => o.Id == id);

        public (bool success, string error, Order? order) Create(CreateOrderDto dto)
        {
            var order = new Order
            {
                Customer = dto.Customer,
                TotalPrice = 0
            };

            decimal total = 0;

            foreach (var item in dto.Items)
            {
                var product = _context.Products.Find(item.ProductId);
                if (product == null)
                    return (false, $"Product with Id={item.ProductId} not found.", null);

                if (product.Stock < item.Quantity)
                    return (false, $"Not enough stock for product '{product.Name}'. Available: {product.Stock}.", null);

                total += product.Price * item.Quantity;
                product.Stock -= item.Quantity;

                order.ProductsInOrders.Add(new ProductsInOrders
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
            }

            order.TotalPrice = total;
            _context.Orders.Add(order);
            _context.SaveChanges();

            return (true, string.Empty, order);
        }
        public bool Delete(int id)
        {
            var order = GetById(id);
            if (order == null) return false;
            foreach (var item in order.ProductsInOrders)
            {
                var product = _context.Products.Find(item.ProductId);
                if (product != null)
                    product.Stock += item.Quantity;
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();
            return true;
        }
    }
}
