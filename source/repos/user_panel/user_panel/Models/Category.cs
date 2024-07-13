
namespace user_panel.Models
{
    public class Category
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }

        public Category(string name)
        {
            Name = name;
            Products = new List<Product>();
        }
    }
}
