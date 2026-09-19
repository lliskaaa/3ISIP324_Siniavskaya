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
                Console.WriteLine("УЧЁТ ТОВАРОВ В МАГАЗИНЕ");
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
                        AddProduct()
                        
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
                        SearchProducts();
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
