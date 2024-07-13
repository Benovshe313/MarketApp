
namespace admin_panel.Models
{
    internal class Admin
    {
        public string Name { get; }
        public string Surname {  get; }
        public string Email {  get; }
        public string Password {  get; }

        public Admin()
        {
            Name = "Charles";
            Surname = "Harrison";
            Email = "charles.harri@gmail.com";
            Password = "Harri_123";
        }
    }
}
