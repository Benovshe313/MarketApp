
using admin_panel.Models;

namespace admin_panel.Helpers
{
    public class AdminManager
    {
        private static Admin admin = new Admin();
        public bool Login(string email, string password)
        {
            return admin.Email == email.ToLower().Trim() && admin.Password == password;
        }
    }
}
