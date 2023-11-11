using ConsoleTools;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using X4GA1C_HFT_2023241.Models;

namespace X4GA1C_HFT_2023241.Client
{
    internal class Program
    {
        static RestService rest;

        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:27376/","laptop");

            var ordererSubMenu = new ConsoleMenu(args, level: 1)
               .Add("List", () => List("Orderer"))
               .Add("Create", () => Create("Orderer"))
               .Add("Delete", () => Delete("Orderer"))
               .Add("Update", () => Update("Orderer"))
               .Add("Exit", ConsoleMenu.Close);

            var orderSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Order"))
                .Add("Create", () => Create("Order"))
                .Add("Delete", () => Delete("Order"))
                .Add("Update", () => Update("Order"))
                .Add("Exit", ConsoleMenu.Close);

            var brandSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Brand"))
                .Add("Create", () => Create("Brand"))
                .Add("Delete", () => Delete("Brand"))
                .Add("Update", () => Update("Brand"))
                .Add("Exit", ConsoleMenu.Close);

            var laptopSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Laptop"))
                .Add("Create", () => Create("Laptop"))
                .Add("Delete", () => Delete("Laptop"))
                .Add("Update", () => Update("Laptop"))
                .Add("Exit", ConsoleMenu.Close);

            var nonCrudSubMenu = new ConsoleMenu(args, level: 1)
                .Add("AvgPriceByBrands", () => AvgPriceByBrands())
                .Add("GetStatByYear", () => GetStatByYear())
                .Add("MostPopularLaptopModels", () => MostPopularLaptopModels())
                .Add("MostPopularBrands", () => MostPopularBrands())
                .Add("MostPayingOrderers", () => MostPayingOrderers())
                .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
                .Add("Brands", () => brandSubMenu.Show())
                .Add("Laptops", () => laptopSubMenu.Show())
                .Add("Orders", () => orderSubMenu.Show())
                .Add("Orderers", () => ordererSubMenu.Show())
                .Add("Non-crud methods", () => nonCrudSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();

        }

        //Non-Crud methods:

        private static void AvgPriceByBrands()
        {
            var temp = rest.Get<KeyValuePair<string, double>>("stat/AvgPriceByBrands");

            foreach (var e in temp)
            {
                Console.WriteLine($"{e.Key} {e.Value}");
            }

            Console.ReadLine();
        }

        private static void GetStatByYear()
        {
            Console.WriteLine("Enter year [like: 2023] : ");

            try
            {
                int year = int.Parse(Console.ReadLine());
                var temp = rest.Get<IEnumerable<dynamic>>(year,"stat/GetStatByYear");
                
                Console.WriteLine($"{year}'s order statistics: \n");



                if (temp.Count() == 0)
                {
                    Console.WriteLine("No data in this year!");
                }
                else
                {
                    foreach (var e in temp)
                    {
                        Console.WriteLine(e);
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        private static void MostPopularLaptopModels()
        {

            var temp = rest.Get<Laptop>("stat/MostPopularLaptopModels");

            foreach (var e in temp)
            {
                Console.WriteLine($"{(e as Laptop).ModelName} [{(e as Laptop).Processor},{(e as Laptop).RAM}GB,{(e as Laptop).Storage}GB] Price: {(e as Laptop).Price}");
            }

            Console.ReadLine();
        }

        private static void MostPopularBrands()
        {

            var temp = rest.Get<Brand>("stat/MostPopularBrands");

            foreach (var e in temp)
            {
                Console.WriteLine($"{e.Name}");
            }

            Console.ReadLine();
        }

        private static void MostPayingOrderers()
        {

            var temp = rest.Get<Orderer>("stat/MostPayingOrderers");

            foreach (var e in temp)
            {
                Console.WriteLine($"Name: {(e as Orderer).Name} Phone: {(e as Orderer).PhoneNumber}");
            }

            Console.ReadLine();
        }


        //Crud methods:

        private static void Update(string str)
        {
            List(str);

            if (str == "Laptop")
            {
                Console.WriteLine($"Enter {str} Id to Update: ");
                int result = int.Parse(Console.ReadLine());

                Laptop temp = rest.Get<Laptop>(result, "laptop");
                
                foreach (var prop in temp.GetType().GetProperties())
                {
                    if (prop.Name == "ModelName")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(temp, answer);
                    }
                    else if (prop.Name == "Processor")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(temp, answer);
                    }
                    else if (prop.Name == "RAM")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(temp, int.Parse(answer));
                    }
                    else if (prop.Name == "Storage")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(temp, int.Parse(answer));
                    }
                    else if (prop.Name == "RAM_Upgradeable")
                    {
                        Console.WriteLine(prop.Name + " :(y/n)");

                        string answer = Console.ReadLine();
                        if (answer.ToLower() == "y")
                        {
                            prop.SetValue(temp, true);
                        }
                        else
                        {
                            prop.SetValue(temp, false);
                        }
                    }
                    else if (prop.Name == "Price")
                    {
                        Console.WriteLine(prop.Name + " :");

                        string answer = Console.ReadLine();
                        prop.SetValue(temp, int.Parse(answer));
                    }
                    else if (prop.Name == "BrandId")
                    {
                        Console.WriteLine(prop.Name + " :");

                        string answer = Console.ReadLine();
                        prop.SetValue(temp, int.Parse(answer));
                    }

                }
                
                rest.Put<Laptop>(temp, "laptop");
            }
            else if (str == "Brand")
            {
                Console.WriteLine($"Enter {str} Id to Update: ");
                int result = int.Parse(Console.ReadLine());

                Brand temp = rest.Get<Brand>(result, "brand");

                foreach (var prop in temp.GetType().GetProperties())
                {
                    if (prop.Name == "Name")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(temp, answer);
                    }
                    else if (prop.Name == "YearOfAppearance")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(temp, int.Parse(answer));
                    }
                }
                rest.Put<Brand>(temp, "brand");
            }
            else if (str == "Order")
            {
                Console.WriteLine($"Enter {str} Id to Update: ");
                int result = int.Parse(Console.ReadLine());

                Order temp = rest.Get<Order>(result, "order");

                foreach (var prop in temp.GetType().GetProperties())
                {
                    if (prop.Name == "Date")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(temp, DateTime.Parse(answer));
                    }
                    else if (prop.Name == "LaptopId")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(temp, int.Parse(answer));
                    }
                    else if (prop.Name == "OrdererId")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(temp, int.Parse(answer));
                    }
                }

                rest.Put<Order>(temp, "order");
            }
            else if (str == "Orderer")
            {
                Console.WriteLine($"Enter {str} Id to Update: ");
                int result = int.Parse(Console.ReadLine());

                Orderer temp = rest.Get<Orderer>(result, "orderer");

                foreach (var prop in temp.GetType().GetProperties())
                {
                    if (prop.Name == "Name")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(temp, answer);
                    }
                    else if (prop.Name == "PhoneNumber")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(temp, answer);
                    }
                }

                rest.Put<Orderer>(temp, "orderer");
            }

            Console.ReadLine();
        }

        private static void Delete(string str)
        {
            // listing items:
            List(str);

            // after that we can delete:
            if (str == "Laptop")
            {
                Console.WriteLine($"Enter {str} Id to Delete: ");

                int result = int.Parse(Console.ReadLine());

                rest.Delete(result,"laptop");
            }
            else if (str == "Brand")
            {
                Console.WriteLine($"Enter {str} Id to Delete: ");

                int result = int.Parse(Console.ReadLine());

                rest.Delete(result, "brand");
            }
            else if (str == "Order")
            {
                Console.WriteLine($"Enter {str} Id to Delete: ");

                int result = int.Parse(Console.ReadLine());

                rest.Delete(result, "order");
            }
            else if (str == "Orderer")
            {
                Console.WriteLine($"Enter {str} Id to Delete: ");

                int result = int.Parse(Console.ReadLine());

                rest.Delete(result, "orderer");
            }

            Console.ReadLine();
        }

        private static void List(string str)
        {
            IEnumerable temp = null;

            if (str == "Laptop")
            {
                temp = rest.Get<Laptop>("laptop");
            }
            else if (str == "Brand")
            {
                temp = rest.Get<Brand>("brand");
            }
            else if (str == "Order")
            {
                temp = rest.Get<Order>("order");
            }
            else if (str == "Orderer")
            {
                temp = rest.Get<Orderer>("orderer");
            }

            if (temp != null)
            {

                foreach (var item in temp)
                {
                    Console.WriteLine(item.ToString());
                }

            }

            Console.ReadLine();
        }

        private static void Create(string str)
        {
            Console.WriteLine("Create a new " + str + " :");

            if (str == "Laptop")
            {
                Laptop newElement = new Laptop();

                foreach (var prop in newElement.GetType().GetProperties())
                {
                    if (prop.Name == "ModelName")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(newElement, answer);
                    }
                    else if (prop.Name == "Processor")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(newElement, answer);
                    }
                    else if (prop.Name == "RAM")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(newElement, int.Parse(answer));
                    }
                    else if (prop.Name == "Storage")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(newElement, int.Parse(answer));
                    }
                    else if (prop.Name == "RAM_Upgradeable")
                    {
                        Console.WriteLine(prop.Name + " :(y/n)");

                        string answer = Console.ReadLine();
                        if (answer.ToLower() == "y")
                        {
                            prop.SetValue(newElement, true);
                        }
                        else
                        {
                            prop.SetValue(newElement, false);
                        }
                    }
                    else if (prop.Name == "Price")
                    {
                        Console.WriteLine(prop.Name + " :");

                        string answer = Console.ReadLine();
                        prop.SetValue(newElement, int.Parse(answer));
                    }
                    else if (prop.Name == "BrandId")
                    {
                        Console.WriteLine(prop.Name + " :");

                        string answer = Console.ReadLine();
                        prop.SetValue(newElement, int.Parse(answer));
                    }

                }

                rest.Post<Laptop>(newElement,"laptop");
            }
            else if (str == "Brand")
            {
                Brand newElement = new Brand();


                foreach (var prop in newElement.GetType().GetProperties())
                {
                    if (prop.Name == "Name")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(newElement, answer);
                    }
                    else if (prop.Name == "YearOfAppearance")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(newElement, int.Parse(answer));
                    }
                }

                rest.Post<Brand>(newElement, "brand");
            }
            else if (str == "Order")
            {
                Order newElement = new Order();

                foreach (var prop in newElement.GetType().GetProperties())
                {
                    if (prop.Name == "Date")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(newElement, DateTime.Parse(answer));
                    }
                    else if (prop.Name == "LaptopId")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(newElement, int.Parse(answer));
                    }
                    else if (prop.Name == "OrdererId")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(newElement, int.Parse(answer));
                    }
                }

                rest.Post<Order>(newElement, "order");

            }
            else if (str == "Orderer")
            {
                Orderer newElement = new Orderer();

                foreach (var prop in newElement.GetType().GetProperties())
                {
                    if (prop.Name == "Name")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(newElement, answer);
                    }
                    else if (prop.Name == "PhoneNumber")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(newElement, answer);
                    }
                }

                rest.Post<Orderer>(newElement, "orderer");

            }
        }
    }
}
