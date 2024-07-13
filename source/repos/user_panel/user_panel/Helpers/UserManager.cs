
using System.Text.Json;
using user_panel.Models;

namespace user_panel.Helpers
{
    internal static class UserManager
    {
        static string path = "users.json";
        public static List<User> users { get; set; } = [];
        public static User? User { get; set; }

        static UserManager()
        {
            if (File.Exists(path))
            {
                var read = File.ReadAllText(path);
                users = JsonSerializer.Deserialize<List<User>>(read);
            }
            if (users is null)
            {
                users = new List<User>();
            }
        }
        public static void CreateAccount(string firstName, string lastName, string birth, string email, string password)
        {
            var user = users.FirstOrDefault(user => user.Email == email!.ToLower().Trim());

            if (user == null)
            {
                user = new User
                {
                    Name = firstName,
                    Surname = lastName,
                    DateOfBirth = DateOnly.ParseExact(birth, "dd.MM.yyyy"),
                    Email = email,
                    Password = password
                };
                users.Add(user);

                var write = JsonSerializer.Serialize(users);
                File.WriteAllText(path, write);
                //User = user;
                return;
            }
            throw new Exception("User already exist");
        }

        public static bool Login(string email, string password)
        {
            User = users.FirstOrDefault(user => user.Email == email.ToLower().Trim() && user.Password == password);

            return User is null ? false : true;
        }
        public static void Logout()
        {
            UserManager.User = null;
        }
    }
}
