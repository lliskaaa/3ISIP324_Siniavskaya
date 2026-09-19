using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP324_Siniavskaya
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
            Console.WriteLine("Цена: " + Price + " руб.");
            Console.WriteLine("Количество: " + Quantity);
            Console.WriteLine("На складе: " + (InStock ? "Да" : "Нет"));
            Console.WriteLine("Категория: " + Category);
        }

    }
    internal class Program
    {
        static List<Product> products = new List<Product>();

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
                Console.WriteLine("\nУЧЁТ ТОВАРОВ В МАГАЗИНЕ");
                Console.WriteLine("1. Добавить товар");
                Console.WriteLine("2. Удалить товар");
                Console.WriteLine("3. Заказать поставку");
                Console.WriteLine("4. Продать товар");
                Console.WriteLine("5. Поиск товаров");
                Console.WriteLine("0. Выход");

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
               
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверная команда.");
                        break;
                }
            }


        }
        static void AddProduct()
        {
            Console.Write("\nВведите название товара: ");
            string name = Console.ReadLine();

            if (name == "")
            {
                Console.WriteLine("Название товара не может быть пустым.");
                return;
            }

            double price;

            while (true)
            {
                Console.Write("Введите цену: ");
                string input = Console.ReadLine();

                if (double.TryParse(input, out price) && price > 0)
                {
                    break;
                }

                Console.WriteLine("Введите положительную цену.");
            }

            int quantity;

            while (true)
            {
                Console.Write("Введите количество: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out quantity) && quantity >= 0)
                {
                    break;
                }

                Console.WriteLine("Количество не может быть отрицательным.");
            }

            Category category;

            while (true)
            {
                Console.WriteLine("Выберите категорию:");
                Console.WriteLine("1. Овощи");
                Console.WriteLine("2. Фрукты");
                Console.WriteLine("3. Ягоды");

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

            Console.WriteLine("Товар добавлен.\n");
            product.PrintInfo();
        }
        static void DeleteProduct()
        {
            Console.WriteLine("Список товаров:");

            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine(products[i].ID + "-" + products[i].Name);
            }

            int ID;

            while (true)
            {
                Console.Write("Введите код товара, который хотите удалить: ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out ID) && ID > 0)
                {
                    break;
                }

                Console.WriteLine("Введите правильный код!");
            }

            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].ID == ID)
                {
                    Console.WriteLine("Вы удаляете товар: " + products[i].Name);
                    products.RemoveAt(i);
                    Console.WriteLine("Товар удалён.");
                    return;
                }
            }

            Console.WriteLine("Товар с таким кодом не найден.");
        }
    }
}
