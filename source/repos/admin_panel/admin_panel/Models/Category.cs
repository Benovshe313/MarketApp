
namespace admin_panel.Models
{
    internal class Category
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }

        public Category() { }
        public Category(string name)
        {
            Name = name;
            Products = new List<Product>();
        }
    }
}
