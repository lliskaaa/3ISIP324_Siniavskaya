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
    }
    internal class Program
    {
        static void Main(string[] args)
        {
           


        }
    }
}
