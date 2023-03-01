using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp27
{
    internal class Program
    {
        public interface ICheck
        {
            void Print(Customer customer, Type type);
        }

        public interface ICustom
        {
            void Init();
            void Add(double amount);
            void TypeMoney();
            void DrawType();
        }
        #region Valute
        public interface Type
        {
            double Convert_Money(double Dengi);
            string ShowType();
        }
        class Evro : Type
        {
            public double Convert_Money(double Dengi)
            {
                return Dengi * 39.15;
            }
            public string ShowType() { return "Евро"; }
        }
        class Dolar : Type
        {
            public double Convert_Money(double Dengi)
            {
                return Dengi * 36.74;
            }
            public string ShowType() { return "Доллар"; }
        }
        class Grivna : Type
        {
            public double Convert_Money(double Dengi)
            {
                return Dengi;
            }
            public string ShowType() { return "Гривна"; }
        }
        #endregion
        public class Customer: ICustom
        {
            public string Imya { get; private set; }
            public string Karta { get; private set; }
            public double Dengi { get; private set; }
            public Type Tip { get; private set; }
            public double Kolvo { get; private set; }
            public Customer() { }
            public Customer(string imya, string karta, int dengi)
            {
                Imya = imya;
                Karta = karta;
                Dengi = dengi;
            }
            public void Init()
            {
                Console.Write($"Klient");
                Imya = Console.ReadLine();
                Console.Write($"Karta");
                Karta = Console.ReadLine();
                Console.Write($"Dengi");
                Dengi = Convert.ToDouble(Console.ReadLine());
            }
            public void Add(double kolvo) => Dengi += kolvo;
            public void Draw(double kolvo)
            {
                if (kolvo > Dengi)
                {
                    Console.WriteLine("Nedostatocho deneg");
                    return;
                }
                Dengi -= kolvo;
            }
            public void DrawType()
            {
                if (Tip == null)
                    Tip = new Grivna();
                Console.Write("Podchet");
                double amount = Convert.ToDouble(Console.ReadLine());
                this.Draw(Tip.Convert_Money(amount));
            }
            public void TypeMoney()
            {
                Console.Write("Tip 1-EUR \t 2-USD \t 3-GRN");
                int buf = Convert.ToInt32(Console.ReadLine());
                switch (buf)
                {
                    case 1:
                        Tip = new Evro(); break;
                    case 2:
                        Tip = new Dolar(); break;
                    case 3:
                        Tip = new Grivna(); break;
                    default:
                        Console.WriteLine("Eror");
                        return;
                }
            }
            public override string ToString()
            {
                return $"Imya{Imya}\n Karta{Karta}";
            }
        }

        class bank
        {
            public void Init(ICustom Klient)
            {
                Klient.Init();
            }
            public void Add(ICustom Klient, double kolvo) => Klient.Add(kolvo);

            public void Check(ICheck check, Customer Klient, Type tip)
            {
                check.Print(Klient, tip);
            }
        }
        #region Check
        class Proverka_Cheka : ICheck
        {
            public void Print(Customer Klient, Type tip)
            {
                Console.WriteLine(Klient.ToString() + $"We don't have winner{Klient.Kolvo:f2}\n Tip" + tip.ShowType());
            }
        }
        class Proverka_Email : ICheck
        {
            public void Print(Customer Klient, Type tip)
            {
                Console.WriteLine("Email: ");
                string buf = Console.ReadLine();
                Console.WriteLine(Klient.ToString() + $"We don't have winner{Klient.Kolvo:f2}\n Tip" + tip.ShowType()+
                    "\nСheck sent to mail: " + buf);
            }
        }
        class Proverka_Sms : ICheck
        {
            public void Print(Customer Klient, Type tip)
            {
                Console.WriteLine("Num: ");
                string buf = Console.ReadLine();
                Console.WriteLine(Klient.ToString() + $"We don't have winner{Klient.Kolvo:f2}\n Tip" + tip.ShowType()+
                    "\nСheck sent to Num: " + buf);
            }
        }
        #endregion
        static void Main(string[] args)
        {
            bank bank = new bank();
        }
    }
}