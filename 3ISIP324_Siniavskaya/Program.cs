using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP324_Siniavskaya
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество операций (от 2 до 40): ");
            int count = Convert.ToInt32(Console.ReadLine());

            while (count < 2 || count > 40)
            {
                Console.Write("Ошибка! Введите число от 2 до 40: ");
                count = Convert.ToInt32(Console.ReadLine());
            }

            List<string> names = new List<string>();
            List<double> prices = new List<double>();

            for (int i = 0; i < count; i++)
            {
                Console.Write("Введите товар или услугу и цену через ';'\n(пример:Хлеб; 90) : ");
                string input = Console.ReadLine();

                string[] np = input.Split(';');
                string name = np[0].Trim();
                double price = Convert.ToDouble(np[1].Trim());

                names.Add(name);
                prices.Add(price);
            }


            int choice = -1;
            while (choice != 0)
            {
                Console.WriteLine("\nМЕНЮ");
                Console.WriteLine("1. Вывод данных");
                Console.WriteLine("2. Статистика");
                Console.WriteLine("3. Сортировка по цене");
                Console.WriteLine("4. Конвертация валюты");
                Console.WriteLine("5. Поиск по названию");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт: ");
                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("\nВАШИ РАСХОДЫ");

                        for (int i = 0; i < names.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. ({names[i]}; {prices[i]})");
                        }
                        break;
                    case 2:
                        double sum = 0;
                        for (int i = 0; i < prices.Count; i++)
                        {
                            sum += prices[i];
                        }

                        double average = sum / prices.Count;
                        double max = prices.Max();
                        double min = prices.Min();
                        Console.WriteLine("\nСТАТИСТИКА");
                        Console.WriteLine("Сумма: " + sum + " руб.");
                        Console.WriteLine("Среднее: " + average + " руб.");
                        Console.WriteLine("Максимальная трата: " + max + " руб.");
                        Console.WriteLine("Минимальная трата: " + min + " руб.");
                        break;
                    case 3:
                        for (int i = 0; i < prices.Count - 1; i++)
                        {
                            for (int j = 0; j < prices.Count - 1 - i; j++)
                            {
                                if (prices[j] > prices[j + 1])
                                {
                                    double tempPrice = prices[j];
                                    prices[j] = prices[j + 1];
                                    prices[j + 1] = tempPrice;

                                    string tempName = names[j];
                                    names[j] = names[j + 1];
                                    names[j + 1] = tempName;
                                }
                            }
                        }
                        Console.WriteLine("\nСОРТИРОВКА ПО ЦЕНЕ");
                        for (int i = 0; i < prices.Count; i++)
                        {
                            Console.WriteLine($"({i + 1}. {names[i]}; {prices[i]}) руб.");
                        }
                        break;
                    case 4:
                        Console.Write("Введите название валюты: ");
                        string currency = Console.ReadLine();
                        Console.Write("Введите курс валюты (рублей за 1 единицу): ");
                        double rate = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("\nКОНВЕРТАЦИЯ");

                        for (int i = 0; i < prices.Count; i++)
                        {
                            double convertedPrice = prices[i] / rate;
                            Console.WriteLine($"({names[i]}; {convertedPrice:F2} {currency})");
                        }
                        break;
                    case 5:
                        Console.Write("Введите название товара или услуги: ");
                        string search = Console.ReadLine();
                        bool found = false;

                        for (int i = 0; i < names.Count; i++)
                        {
                            if (names[i].ToLower().Contains(search.ToLower()))
                            {
                                Console.WriteLine($"({names[i]}; {prices[i]}) руб.");
                                found = true;
                            }
                        }
                        if (!found)
                        {
                            Console.WriteLine("Товар или услуга не найдены.");
                        }
                        break;
                    case 0:
                        Console.WriteLine("Программа завершена.");
                        break;
                    default:
                        Console.WriteLine("Такого пункта нет.");
                        break;
                }
            }


        }
    }
}
