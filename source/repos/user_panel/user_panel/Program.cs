
using System.Globalization;
using System.Text.Json;
using user_panel.Models;

namespace user_panel
{
    internal class Program
    {
        static List<Category> categories = new List<Category>();

        public static void CreateAccountPage()
        {
        Register:
            Console.WriteLine("Create account");
            Console.Write("First name: ");
            var firstName = Console.ReadLine();
            Console.Write("Last name: ");
            var lastName = Console.ReadLine();
            Console.Write("Date of birth (dd.MM.yyyy): ");
            var birth = Console.ReadLine();
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            try
            {
                Helpers.UserManager.CreateAccount(firstName!, lastName!, birth!, email!.ToLower().Trim(), password!);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                goto Register;
            }
        }

        public static bool LoginPage()
        {
            while (true)
            {
                Console.WriteLine("Login");
                Console.Write("Email: ");
                var loginEmail = Console.ReadLine();
                Console.Write("Password: ");
                var loginPassword = Console.ReadLine();

                if (Helpers.UserManager.Login(loginEmail!, loginPassword!))
                {
                    Console.WriteLine("Login successful!");
                    if (Helpers.UserManager.User is not null)
                    {
                        Console.WriteLine($"Welcome {Helpers.UserManager.User.Name}!");
                        Thread.Sleep(2000);
                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("Invalid email or password");
                }
            }
        }
        public static void LoginMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Profile");
                Console.WriteLine("2. Categories");
                Console.WriteLine("3. Basket");
                Console.WriteLine("4. Logout");

                Console.Write("Make choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        ShowProfile();
                        break;
                    case "2":
                        Console.Clear();
                        ShowCategories();
                        break;
                    case "3":
                        Console.Clear();
                        Basket.ShowBasket();
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        public static void MainPage()
        {
            Console.WriteLine("1. Create account");
            Console.WriteLine("2. Login");
        }

        public static void ShowProfile()
        {
            Console.WriteLine($"Name: {Helpers.UserManager.User.Name}");
            Console.WriteLine($"Surname: {Helpers.UserManager.User.Surname}");
            Console.WriteLine($"Date of Birth: {Helpers.UserManager.User.DateOfBirth:dd.MM.yyyy}");
            Console.WriteLine($"Email: {Helpers.UserManager.User.Email}");
            Console.WriteLine("1. Update profile");
            Console.WriteLine("2. Change password");
            Console.WriteLine("3. History");
            Console.WriteLine("4. Return");
            Console.Write("Make choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    UpdateProfile();
                    break;
                case "2":
                    Console.Clear();
                    ChangePassword();
                    break;
                case "3":
                    History();
                    Console.Clear();
                    break;
                case "4":
                    Console.Clear();
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }

        public static void UpdateProfile()
        {
            Console.WriteLine("Update profile");
            Console.WriteLine("1. Update first name");
            Console.WriteLine("2. Update last name");
            Console.WriteLine("3. Update Date of Birth");
            Console.WriteLine("4. Update email");
            Console.WriteLine("5. Return");
            Console.Write("Make choice: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    Console.Write("New first name: ");
                    var newName = Console.ReadLine();
                    if (newName == Helpers.UserManager.User.Name)
                    {
                        Console.WriteLine("New first name is same with previous one");
                    }
                    else
                    {
                        Helpers.UserManager.User.Name = newName;
                        Console.WriteLine("First name updated");
                    }
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;

                case "2":
                    Console.Clear();
                    Console.Write("New last name: ");
                    var newSurname = Console.ReadLine();
                    if (newSurname == Helpers.UserManager.User.Surname)
                    {
                        Console.WriteLine("New last name is same with previous one");
                    }
                    else
                    {
                        Helpers.UserManager.User.Surname = newSurname;
                        Console.WriteLine("Last name updated");
                    }
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;

                case "3":
                    Console.Clear();
                    Console.Write("New Date of Birth (dd.MM.yyyy): ");
                    if (DateOnly.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly dateOfBirth))
                    {
                        if (dateOfBirth == Helpers.UserManager.User.DateOfBirth)
                        {
                            Console.WriteLine("New Date of Birth is same with previous one");
                        }
                        else
                        {
                            Helpers.UserManager.User.DateOfBirth = dateOfBirth;
                            Console.WriteLine("Date of Birth updated");
                        }
                        Thread.Sleep(2000);
                        Console.Clear();
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                    break;

                case "4":
                    Console.Clear();
                    Console.Write("New email: ");
                    var newEmail = Console.ReadLine();
                    if (newEmail.ToLower().Trim() == Helpers.UserManager.User.Email)
                    {
                        Console.WriteLine("New email is the same with previous one");
                    }
                    else
                    {
                        Helpers.UserManager.User.Email = newEmail.ToLower().Trim();
                        Console.WriteLine("Email updated");
                    }
                    Thread.Sleep(2000);
                    Console.Clear();
                    break;

                case "5":
                    Console.Clear();
                    return;

                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
            SaveUpdateProfile();
        }
        public static void SaveUpdateProfile()
        {
            try
            {
                string profilePath = @"C:\Users\User\source\repos\user_panel\user_panel\bin\Debug\net8.0\users.json";

                List<User> users;
                if (File.Exists(profilePath))
                {
                    string json = File.ReadAllText(profilePath);
                    users = JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
                }
                else
                {
                    users = new List<User>();
                }

                var currentUser = Helpers.UserManager.User;
                var userIndex = users.FindIndex(u => u.Email == currentUser.Email);
                if (userIndex != -1)
                {
                    users[userIndex] = currentUser;
                }
                else
                {
                    users.Add(currentUser);
                }

                string updatedJson = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(profilePath, updatedJson);

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void ChangePassword()
        {
            Console.Write("Current password: ");
            var currentPassword = Console.ReadLine();

            if (currentPassword != Helpers.UserManager.User.Password)
            {
                Console.WriteLine("Current password is wrong");
                return;
            }

            Console.Write("New password: ");
            var newPassword = Console.ReadLine();

            if (currentPassword == newPassword)
            {
                Console.WriteLine("New password is same with current password");
                Thread.Sleep(2000);
                Console.Clear();
                return;
            }

            Console.Write("Confirm new password: ");
            var confirmPassword = Console.ReadLine();

            if (newPassword != confirmPassword)
            {
                Console.WriteLine("Passwords do not match");
                return;
            }

            Helpers.UserManager.User.Password = newPassword;
            SaveUpdateProfile();
            Console.WriteLine("Password changed");
            Thread.Sleep(2000);
            Console.Clear();
        }

        public static void History()
        {
            string historyPath = @"C:\Users\User\source\repos\user_panel\user_panel\bin\Debug\net8.0\purchaseHistory.json";
            if (File.Exists(historyPath))
            {
                string json = File.ReadAllText(historyPath);
                List<Bought> items = JsonSerializer.Deserialize<List<Bought>>(json) ?? new List<Bought>();

                var userHistory = items.Where(item => item.UserId == Helpers.UserManager.User.Email).ToList();

                if (userHistory.Count == 0)
                {
                    Console.WriteLine("No item");
                }
                else
                {
                    foreach (var item in userHistory)
                    {
                        Console.WriteLine($"Date: {item.Date}");

                        foreach (var product in item.Products)
                        {
                            Console.WriteLine($"{product.Name}, Price: {product.Price} azn, Quantity: {product.Quantity}");
                        }
                        Console.WriteLine($"Total Amount: {item.TotalAmount} azn");
                    }
                }
            }
            else
            {
                Console.WriteLine("History is empty");
            }

            Console.WriteLine("Press any key to go menu ..");
            Console.ReadKey();
        }


        public static void ShowCategories()
        {
            Console.WriteLine("Categories:");

            for (int i = 0; i < categories.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {categories[i].Name}");
            }
            Console.Write("Enter row number of category: ");
            int rowNum;
            if (int.TryParse(Console.ReadLine(), out rowNum) && rowNum >= 1 && rowNum <= categories.Count)
            {
                var optionCateg = categories[rowNum - 1];
                Console.WriteLine($"Category: {optionCateg.Name}");

                Console.WriteLine("Products:");

                var existProd = optionCateg.Products.Where(amount => amount.Quantity > 0).ToList();
                if (existProd.Count == 0)
                {
                    Console.WriteLine("No product in stock");
                    Console.WriteLine("Press any key to go back ..");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }

                for (int i = 0; i < existProd.Count; i++)
                {
                    var product = existProd[i];
                    Console.WriteLine($" {i + 1}. {product.Name}, {product.Price} azn, Quantity:{product.Quantity}, Desc: {product.Description}");
                }

                Console.Write("Enter row number of item add to basket else press 0: ");
                int addBasket;
                if (int.TryParse(Console.ReadLine(), out addBasket) && addBasket >= 1 && addBasket <= existProd.Count)
                {
                    var selectedProduct = existProd[addBasket - 1];
                    Console.Write("Enter quantity: ");
                    if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                    {
                        if (quantity <= selectedProduct.Quantity)
                        {
                            selectedProduct.Quantity = quantity;
                            Basket.AddToBasket(selectedProduct);
                        }
                        else
                        {
                            Console.WriteLine("There is not enough product");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid quantity");
                    }
                }
                else if (addBasket != 0)
                {
                    Console.WriteLine("Invalid product selected");
                }
            }
            else
            {
                Console.WriteLine("Invalid category selected");
            }

            Console.WriteLine("Press any key to go back ..");
            Console.ReadKey();
            Console.Clear();
        }

        public static void LoadCategories()
        {
            try
            {
                string filePath = @"C:\Users\User\source\repos\admin_panel\admin_panel\bin\Debug\net8.0\categories.json";
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    categories = JsonSerializer.Deserialize<List<Category>>(json);

                    foreach (var category in categories)
                    {
                        Console.WriteLine($"Category: {category.Name}");
                        foreach (var product in category.Products)
                        {

                            Console.WriteLine($" {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}, Desc: {product.Description}");
                        }
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static class Basket
        {
            static string basketPath = @"C:\Users\User\source\repos\user_panel\user_panel\bin\Debug\net8.0\basket.json";

            public static List<Product> BasketItems { get; set; } = new List<Product>();

            public static void AddToBasket(Product product)
            {
                var existProduct = BasketItems.FirstOrDefault(name => name.Name.ToLower() == product.Name.ToLower());
                if (existProduct != null)
                {
                    existProduct.Quantity += product.Quantity;
                }
                else
                {
                    BasketItems.Add(product);
                }

                Console.WriteLine($"{product.Name} added to basket");
                SaveBasket();
            }


            public static void RemoveFromBasket()
            {
                if (BasketItems.Count == 0)
                {
                    Console.WriteLine("Basket is empty");
                    return;
                }
                for (int i = 0; i < BasketItems.Count; i++)
                {
                    var product = BasketItems[i];
                    Console.WriteLine($" {i + 1}. {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}, Desc: {product.Description}");
                }

                Console.Write("Enter the number of product to remove: ");
                if (int.TryParse(Console.ReadLine(), out int prod) && prod > 0 && prod <= BasketItems.Count)
                {
                    var removeProd = BasketItems[prod - 1];
                    BasketItems.RemoveAt(prod - 1);
                    Console.WriteLine($"{removeProd.Name} removed from basket");
                    SaveBasket();
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
                Thread.Sleep(2000);
                Console.Clear();    
            }

            public static void ShowBasket()
            {
                if (BasketItems.Count == 0)
                {
                    Console.WriteLine("Basket is empty");
                }
                else
                {
                    double totalAmount = 0;
                    for (int i = 0; i < BasketItems.Count; i++)
                    {
                        var product = BasketItems[i];
                        Console.WriteLine($" {i + 1}. {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}, Desc: {product.Description}");
                        totalAmount += product.Price * product.Quantity;
                    }
                    Console.WriteLine($"Total Amount: {totalAmount} azn");

                    Console.WriteLine("1. Make payment");
                    Console.WriteLine("2. Go to menu");
                    Console.WriteLine("3. Remove From Basket");
                    Console.Write("Make choice: ");
                    var opt = Console.ReadLine();

                    switch (opt)
                    {
                        case "1":
                            Console.Clear();
                            CalculateTotal(totalAmount);
                            Thread.Sleep(2000);
                            Console.Clear();
                            break;
                        case "2":
                            Console.Clear();
                            break;
                        case "3":
                            RemoveFromBasket();
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }
            }

            public static void CalculateTotal(double totalAmount)
            {
                Console.WriteLine($"Total amount: {totalAmount} azn");
                while (true)
                {
                    Console.WriteLine("Enter payment:");
                    var pay = Console.ReadLine();
                    if (double.TryParse(pay, out double payment))
                    {
                        if (payment == totalAmount)
                        {
                            Console.WriteLine("Thanks for the payment");
                            SaveBought(totalAmount);
                            BasketItems.Clear();
                            SaveBasket();
                            Thread.Sleep(2000);
                            Console.Clear();
                            break;
                        }
                        else if (payment > totalAmount)
                        {
                            Console.WriteLine($"Take your change: {payment - totalAmount} azn");
                            SaveBought(totalAmount);
                            BasketItems.Clear();
                            SaveBasket();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Payment not enough");
                        }
                    }
                }
            }

            public static void SaveBasket()
            {
                try
                {
                    string json = JsonSerializer.Serialize(BasketItems, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(basketPath, json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            public static void SaveBought(double totalAmount)
            {
                var purchase = new Bought
                {
                    Date = DateTime.Now,
                    Products = new List<Product>(BasketItems),
                    TotalAmount = totalAmount,
                    UserId = Helpers.UserManager.User.Email
                };

                string filePath = @"C:\Users\User\source\repos\user_panel\user_panel\bin\Debug\net8.0\purchaseHistory.json";
                List<Bought> purchaseHistory;
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    purchaseHistory = JsonSerializer.Deserialize<List<Bought>>(json) ?? new List<Bought>();
                }
                else
                {
                    purchaseHistory = new List<Bought>();
                }

                purchaseHistory.Add(purchase);

                string updatedJson = JsonSerializer.Serialize(purchaseHistory, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, updatedJson);
            }

            public static void LoadBasket()
            {
                try
                {
                    if (File.Exists(basketPath))
                    {
                        string json = File.ReadAllText(basketPath);
                        BasketItems = JsonSerializer.Deserialize<List<Product>>(json);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }


        static void Main(string[] args)
        {
            bool loggedin = false;
            bool exit = false;
            while (true)
            {
                if (!loggedin)
                {
                    MainPage();
                    Console.Write("Make choice: ");
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Clear();
                            CreateAccountPage();
                            loggedin = LoginPage();
                            break;
                        case "2":
                            Console.Clear();
                            loggedin = LoginPage();
                            break;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }
                else
                {
                    Console.Clear();
                    LoadCategories();
                    Console.Clear();
                    Basket.LoadBasket(); 
                    LoginMenu();
                    Basket.SaveBasket(); 
                    loggedin = false;
                }
            }
        }
    }
}
