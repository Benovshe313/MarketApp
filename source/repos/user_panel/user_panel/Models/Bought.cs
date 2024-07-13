
namespace user_panel.Models
{
    public class Bought
    {
        public DateTime Date { get; set; }
        public List<Product> Products { get; set; }
        public double TotalAmount { get; set; }
        public string UserId { get; set; }
    }
}
