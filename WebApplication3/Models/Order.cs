using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopAPI.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Customer { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }

        public ICollection<ProductsInOrders> ProductsInOrders { get; set; } = new List<ProductsInOrders>();
    }
}
