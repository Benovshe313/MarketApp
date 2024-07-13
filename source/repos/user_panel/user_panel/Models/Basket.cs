
namespace user_panel.Models
{
    public class Basket
    {
        public List<Product> Products { get; set; }
        public Basket()
        {
            Products = new List<Product>();
        }
    }
}
