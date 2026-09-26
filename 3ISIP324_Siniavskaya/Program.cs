using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    public enum Category
    {
        Vegetab = 1,
        Fruits = 2,
        Berri = 3
    }

    class Product
    {
        public int ID { get; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }
        private static int nextID = 1;

        public Product(string name, double price, int quantity, Category category)
        {
            ID = nextID;
            nextID++;
            Name = name;
            Price = price;
            Quantity = quantity;
            Category = category;

        }
        public bool InStock
        {
            get
            {
                return Quantity > 0;
            }
        }
        public void PrintInfo()
        {
            Console.WriteLine("Код: " + ID);
            Console.WriteLine("Название: " + Name);
            Console.WriteLine("Цена: " + Price + "руб.");
            Console.WriteLine("Количество: " + Quantity);
            Console.WriteLine("На складе: " + (InStock ? "Да" : "Нет"));
            Console.WriteLine("Категория: " + Category);
        }

    }
    class Sale
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double Total { get; set; }
    }
    internal class Program
    {
        static List<Product> products = new List<Product>();
        static Stack<Sale> salesHistory = new Stack<Sale>();
        static List<Sale> sales = new List<Sale>();

        static void AddProduct()
        {
            string name;
            while (true)
            {
                Console.Write("Введите название товара: ");
                name = Console.ReadLine();
                if (name != "")
                {
                    break;
                }
                Console.WriteLine("Название товара не может быть пустым.");
            }

            double price;
            while (true)
            {
                Console.WriteLine("Введите цену: ");
                string input = Console.ReadLine();
                if (double.TryParse(input, out price) && price > 0)
                {
                    break;
                }
                Console.WriteLine("Введите положительную цену!");
            }

            int quantity;
            while (true)
            {
                Console.WriteLine("Введите количество: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out quantity) && quantity >= 0)
                {
                    break;
                }
                Console.WriteLine("Количество не может быть отрицательным!");
            }
            Category category;
            while (true)
            {
                Console.WriteLine("Выберите категорию:\n 1-Овощи\n 2-Фрукты\n 3-Ягоды");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        category = Category.Vegetab;
                        break;
                    case "2":
                        category = Category.Fruits;
                        break;
                    case "3":
                        category = Category.Berri;
                        break;
                    default:
                        Console.WriteLine("Неверная категория!");
                        continue;
                }
                break;
            }
            Product product = new Product(name, price, quantity, category);
            products.Add(product);
            Console.WriteLine("Товар добавлен.");
            product.PrintInfo();


        }
        static void DeleteProduct()
        {
            Console.WriteLine("Список товаров: ");
            for (int i = 0; i<products.Count; i++)
            {
                Console.WriteLine("ID-" + products[i].ID + " " + products[i].Name);
            }
            Console.WriteLine("Как вы хотите удалить товар?");
            Console.WriteLine(" 1 — По коду");
            Console.WriteLine(" 2 — По названию");
            Console.Write("\nВаш выбор: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    int ID;
                    while (true)
                    {
                        Console.Write("\nВведите код товара: ");
                        string intput = Console.ReadLine();
                        if (int.TryParse(intput, out ID) && ID > 0)
                        {
                            break;
                        }
                        Console.WriteLine("Введите правильный код!");
                    }
                    for (int i = 0; i < products.Count; i++)
                    {
                        if (products[i].ID == ID)
                        {
                            Console.WriteLine($"Вы удаляете товар: {products[i].Name} ");
                            products.RemoveAt(i);
                            Console.WriteLine("Товар удален.");
                            return;
                        }
                    }
                    Console.WriteLine("Товар с таким кодом не найден!");
                    break;
                case "2":
                    Console.Write("\nВведите название товара: ");
                    string name = Console.ReadLine();
                    for (int i = 0; i > products.Count; i++)
                    {
                        if (products[i].Name == name)
                        {
                            Console.WriteLine($"Вы удаляете товар: {products[i].Name}");
                            products.RemoveAt(i);
                            Console.WriteLine("Товар удален.");
                            return;
                        }
                    }
                    Console.WriteLine("Товар с таким названием не найден.");
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }
        }
        static void SupplyProduct()
        {
            int ID;
            while (true)
            {
                Console.Write("Введите код товара: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out ID) && ID > 0)
                {
                    break;
                }
                Console.WriteLine("Введите правильный код.");
            }
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].ID == ID)
                {
                    int quantity;

                    while (true)
                    {
                        Console.Write("Введите количество поставки: ");
                        string input = Console.ReadLine();

                        if (int.TryParse(input, out quantity) && quantity > 0)
                        {
                            break;
                        }
                        Console.WriteLine("Количество должно быть больше 0.");
                    }
                    products[i].Quantity += quantity;
                    Console.WriteLine("Поставка оформлена.");
                    Console.WriteLine("Новое количество: " + products[i].Quantity);
                    return;
                }
            }
            Console.WriteLine("Товар с таким кодом не найден.");
        }
        static void SellProduct()
        {
            int ID;

            while (true)
            {
                Console.Write("Введите код товара: ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out ID) && ID > 0)
                {
                    break;
                }
                Console.WriteLine("Введите правильный код.");
            }
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].ID == ID)
                {
                    if (!products[i].InStock)
                    {
                        Console.WriteLine("Товара нет на складе.");
                        return;
                    }

                    int quantity;
                    while (true)
                    {
                        Console.Write("Введите количество товара для продажи: ");
                        string input = Console.ReadLine();
                        if (int.TryParse(input, out quantity) && quantity > 0)
                        {
                            break;
                        }

                        Console.WriteLine("Количество должно быть больше 0.");
                    }
                    if (quantity > products[i].Quantity)
                    {
                        Console.WriteLine("Недостаточно товара на складе.");
                        return;
                    }

                    products[i].Quantity -= quantity;
                    Sale sale = new Sale();
                    sale.Product = products[i];
                    sale.Quantity = quantity;
                    sale.Total = products[i].Price * quantity;
                    salesHistory.Push(sale);
                    sales.Add(sale);

                    Console.WriteLine("Товар продан.");
                    Console.WriteLine("Осталось на складе: " + products[i].Quantity);
                    return;
                }
            }
            Console.WriteLine("Товар с таким кодом не найден.");
        }
        static void UndoLastSale()
        {
            if (salesHistory.Count == 0)
            {
                Console.WriteLine("История продаж пуста.");
                return;
            }
            Sale sale = salesHistory.Pop();
            sale.Product.Quantity += sale.Quantity;
            Console.WriteLine("Последняя продажа отменена.");
            Console.WriteLine("Товар: " + sale.Product.Name);
            Console.WriteLine("Возвращено на склад: " + sale.Quantity);
        }
        static void SearchProducts()
        {
            Console.WriteLine("Как вы хотите найти товар?");
            Console.WriteLine("1 — По коду");
            Console.WriteLine("2 — По названию");
            Console.WriteLine("3 — По категории");
            Console.Write("Ваш выбор: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    int ID;
                    while (true)
                    {
                        Console.Write("Введите код товара: ");
                        string input = Console.ReadLine();
                        if (int.TryParse(input, out ID) && ID > 0)
                        {
                            break;
                        }
                        Console.WriteLine("Введите правильный код.");
                    }
                    for (int i = 0; i < products.Count; i++)
                    {
                        if (products[i].ID == ID)
                        {
                            products[i].PrintInfo();
                            return;
                        }
                    }
                    Console.WriteLine("Товар с таким кодом не найден!");
                    break;

                case "2":
                    Console.Write("Введите название товара: ");
                    string name = Console.ReadLine();
                    for (int i = 0; i < products.Count; i++)
                    {
                        if (products[i].Name == name)
                        {
                            products[i].PrintInfo();
                            return;
                        }
                    }
                    Console.WriteLine("Товар с таким названием не найден!");
                    break;

                case "3":
                    Console.WriteLine("Выберите категорию:");
                    Console.WriteLine("1. Овощи");
                    Console.WriteLine("2. Фрукты");
                    Console.WriteLine("3. Ягоды");
                    Console.Write("Ваш выбор: ");
                    string categoryChoice = Console.ReadLine();
                    Category category;
                    switch (categoryChoice)
                    {
                        case "1":
                            category = Category.Vegetab;
                            break;
                        case "2":
                            category = Category.Fruits;
                            break;
                        case "3":
                            category = Category.Berri;
                            break;
                        default:
                            Console.WriteLine("Неверная категория.");
                            return;
                    }
                    for (int i = 0; i < products.Count; i++)
                    {
                        if (products[i].Category == category)
                        {
                            products[i].PrintInfo();
                        }
                    }
                    break;
                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
        static void SalesReport()
        {
            if (sales.Count == 0)
            {
                Console.WriteLine("Продаж пока нет.");
                return;
            }
            double totalSales = 0;
            Console.WriteLine("ОТЧЁТ О ПРОДАЖАХ");
            for (int i = 0; i < sales.Count; i++)
            {
                Console.WriteLine("Товар: " + sales[i].Product.Name);
                Console.WriteLine("Количество: " + sales[i].Quantity);
                Console.WriteLine("Сумма: " + sales[i].Total + " руб.");

                totalSales += sales[i].Total;
            }

            Console.WriteLine("Общая сумма продаж: " + totalSales + " руб.");
        }


        static void Main(string[] args)
        {
            products.Add(new Product("Яблоки", 150, 10, Category.Fruits));
            products.Add(new Product("Морковь", 80, 15, Category.Vegetab));
            products.Add(new Product("Клубника", 300, 8, Category.Berri));
            products.Add(new Product("Бананы", 120, 12, Category.Fruits));
            products.Add(new Product("Помидоры", 200, 7, Category.Vegetab));
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("УЧЁТ ТОВАРОВ В МАГАЗИНЕ");
                Console.WriteLine("1 — Добавить товар");
                Console.WriteLine("2 — Удалить товар");
                Console.WriteLine("3 — Заказать поставку");
                Console.WriteLine("4 — Продать товар");
                Console.WriteLine("5 — Отменить последнюю продажу");
                Console.WriteLine("6 — Поиск товаров");
                Console.WriteLine("7 — Отчёт о продажах");
                Console.WriteLine("0 — Выход");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProduct();
                        break;
                    case "2":
                        DeleteProduct();
                        break;
                    case "3":
                        SupplyProduct();
                        break;
                    case "4":
                        SellProduct();
                        break;
                    case "5":
                        UndoLastSale();
                        break;
                    case "6":
                        SearchProducts();
                        break;
                    case "7":
                        SalesReport();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверная команда.");
                        break;

                }
            }
        }
    }
}