using System;
using System.Collections.Generic;
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
        public double Prise { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
           


        }
    }
}
