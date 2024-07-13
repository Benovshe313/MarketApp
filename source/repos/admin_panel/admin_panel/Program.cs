using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using admin_panel.Models;

namespace admin_panel
{
    internal class Program
    {
        static List<Category> categories = new List<Category>();

        static string path = @"C:\Users\User\source\repos\admin_panel\admin_panel\bin\Debug\net8.0\categories.json";

        public static void ReadFromFile()
        {
            try
            {
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    categories = JsonSerializer.Deserialize<List<Category>>(json);
                }
                else
                {
                    categories = new List<Category>
                    {
                        new Category("Dairy products")
                        {
                            Products = new List<Product>
                            {
                                new Product("Cheese", 13.99, 20, "NOVA GAUDA yellow cheese"),
                                new Product("Butter", 16.99, 20, "VIOLETTO 100% natural butter"),
                                new Product("Yoghurt", 3.49, 20, "YAYLA Sour Village Natural Yoghurt")
                            }
                        },
                        new Category("Fruit, vegetable")
                        {
                            Products = new List<Product>
                            {
                                new Product("Apple", 2.19, 50, "Red apple FUJI"),
                                new Product("Pepper", 3.59, 40, "Green pepper"),
                                new Product("Cabbage", 1.15, 20, "White cabbage")
                            }
                        },
                        new Category("Flour products")
                        {
                            Products = new List<Product>
                            {
                                new Product("Factory bread", 0.95, 25, "Sliced white bread"),
                                new Product("Diabetic bread", 2.30, 25, "IVANOVKA 100% Natural")
                            }
                        },
                        new Category("Beverage")
                        {
                            Products = new List<Product>
                            {
                                new Product("Water", 0.95, 50, "SIRAB Still water"),
                                new Product("Juice", 2.20, 50, "NATURA MULTIVITAMIN no sugar mixed fruits"),
                                new Product("Lemonade", 2.05, 50, "NATAKHTARI Pear lemonade")
                            }
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void WriteToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(categories, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void LoginPage(out string email, out string password)
        {
            Console.WriteLine("Login");
            Console.Write("Email: ");
            email = Console.ReadLine();
            Console.Write("Password: ");
            password = Console.ReadLine();
        }

        public static void MenuPage()
        {
            Console.WriteLine("MENU");
            Console.WriteLine("1. Categories");
            Console.WriteLine("2. Add product");
            Console.WriteLine("3. Add category");
            Console.WriteLine("4. Report");
            Console.WriteLine("5. Exit");
        }

        public static void ShowCategories()
        {
            Console.WriteLine("Categories");
            for (int i = 0; i < categories.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {categories[i].Name}");
            }
        }

        public static void ShowProduct(string categoryChoice)
        {
            if (!int.TryParse(categoryChoice, out int categoryIndex) || categoryIndex < 1 || categoryIndex > categories.Count)
            {
                Console.WriteLine("Invalid choice");
                Thread.Sleep(2000);
                return;
            }

            var category = categories[categoryIndex - 1];

            if (category.Products.Count == 0)
            {
                Console.WriteLine($"No product in {category.Name}. Add product first");
                Thread.Sleep(2000);
                return;
            }

            for (int i = 0; i < category.Products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {category.Products[i].Name}");
            }

            Console.Write("Choose product: ");
            var productChoice = Console.ReadLine();
            Console.Clear();
            AboutProduct(category, productChoice);
        }

        public static void DetailsProducts(string name, double price, int quantity, string info)
        {
            Console.WriteLine($"Product: {name}");
            Console.WriteLine($"Price: {price} AZN");
            Console.WriteLine($"Quantity: {quantity}");
            Console.WriteLine($"Desc: {info}");
        }

        public static void AboutProduct(Category category, string productChoice)
        {
            if (!int.TryParse(productChoice, out int productIndex) || productIndex < 1 || productIndex > category.Products.Count)
            {
                Console.WriteLine("Invalid choice");
                Thread.Sleep(2000);
                return;
            }

            var selectProduct = category.Products[productIndex - 1];
            DetailsProducts(selectProduct.Name, selectProduct.Price, selectProduct.Quantity, selectProduct.Description);

            Console.WriteLine("Do you want make changes? 1.Yes 2.No \n1 or 2 ? ");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.Clear();
                Console.Write("New name: ");
                string newName = Console.ReadLine();
                Console.Write("New price: ");
                double newPrice;
                while (!double.TryParse(Console.ReadLine(), out newPrice))
                {
                    Console.WriteLine("Invalid price");
                    Console.Write("New price: ");
                }
                Console.Write("New quantity: ");
                int newQuantity;
                while (!int.TryParse(Console.ReadLine(), out newQuantity))
                {
                    Console.WriteLine("Invalid quantity");
                    Console.Write("New quantity: ");
                }
                Console.Write("New description: ");
                string newDesc = Console.ReadLine();

                selectProduct.Name = newName;
                selectProduct.Price = newPrice;
                selectProduct.Quantity = newQuantity;
                selectProduct.Description = newDesc;
                Console.WriteLine("Changes made");
                Thread.Sleep(2000);
            }
        }
        public static void AddProduct(string categoryChoice)
        {
            if (!int.TryParse(categoryChoice, out int categoryIndex) || categoryIndex < 1 || categoryIndex > categories.Count)
            {
                Console.WriteLine("Invalid choice");
                Thread.Sleep(2000);
                return;
            }

            var category = categories[categoryIndex - 1];
            Console.Write("Product name: ");
            string productName = Console.ReadLine();
            Console.Write("Product price: ");
            double productPrice;
            while (!double.TryParse(Console.ReadLine(), out productPrice))
            {
                Console.WriteLine("Invalid price");
                Console.Write("Product price: ");
            }
            Console.Write("Product quantity: ");
            int productQuantity;
            while (!int.TryParse(Console.ReadLine(), out productQuantity))
            {
                Console.WriteLine("Invalid quantity");
                Console.Write("Product quantity: ");
            }
            Console.Write("Product description: ");
            string productDesc = Console.ReadLine();

            category.Products.Add(new Product(productName, productPrice, productQuantity, productDesc));

            Console.WriteLine("Product added");
        }

        public static void AddCategory()
        {
            Console.Write("New category name: ");
            string categoryName = Console.ReadLine();
            if (categories.Any(name => name.Name.ToLower() == categoryName.ToLower()))
            {
                Console.WriteLine("Category already exist");
                return;
            }

            categories.Add(new Category(categoryName));
            Console.WriteLine("Category added");
        }

        public static void ShowReport()
        {
            string historyPath = @"C:\Users\User\source\repos\user_panel\user_panel\bin\Debug\net8.0\purchaseHistory.json";
            if (File.Exists(historyPath))
            {
                string json = File.ReadAllText(historyPath);
                List<Bought> bought = JsonSerializer.Deserialize<List<Bought>>(json) ?? new List<Bought>();

                if (bought.Count == 0)
                {
                    Console.WriteLine("No report");
                    Thread.Sleep(2000);
                }
                else
                {
                    var report = bought
                        .GroupBy(d => d.Date.ToString("dd.MM.yyyy"))
                        .Select(result => new
                        {
                            Date = result.Key,
                            TotalAmount = result.Sum(sum => sum.TotalAmount)
                        }).ToList();
                    
                    foreach (var details in report)
                    {
                        Console.WriteLine($"Date: {details.Date}, Amount: {details.TotalAmount} azn");
                    }
                }
            }

            Console.WriteLine("Press any key to return ..");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            Helpers.AdminManager adminManager = new Helpers.AdminManager();
            ReadFromFile();

            string email, password;
            LoginPage(out email, out password);
            bool login = adminManager.Login(email.ToLower().Trim(), password);
            Console.WriteLine(login ? "Login successful!" : "Login failed");

            if (login)
            {
                while (true)
                {
                    Console.Clear();
                    MenuPage();
                    Console.Write("Make choice: ");
                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.Clear();
                            ShowCategories();
                            Console.Write("Choose category: ");
                            var categoryChoice = Console.ReadLine();
                            ShowProduct(categoryChoice);
                            break;
                        case "2":
                            Console.Clear();
                            ShowCategories();
                            Console.Write("Choose category to add product: ");
                            var addCategoryChoice = Console.ReadLine();
                            AddProduct(addCategoryChoice);
                            Thread.Sleep(2000);
                            break;
                        case "3":
                            Console.Clear();
                            AddCategory();
                            Thread.Sleep(2000);
                            break;
                        case "4":
                            Console.Clear();
                            ShowReport();
                            break;
                        case "5":
                            Console.Clear();
                            WriteToFile();
                            return;
                        default:
                            Console.WriteLine("Invalid choice");
                            break;
                    }
                }
            }
        }
    }
}
