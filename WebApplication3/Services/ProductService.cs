using ShopAPI.Models;

namespace ShopAPI.Services
{
    public class ProductService
    {
        private readonly ShopDbContext _context;

        public ProductService(ShopDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAll() => _context.Products.ToList();
        public Product? GetById(int id) => _context.Products.Find(id);

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void Update() => _context.SaveChanges();

        public void Delete(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}
