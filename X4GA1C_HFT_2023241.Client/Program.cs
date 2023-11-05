using ConsoleTools;
using System;
using System.Collections;
using System.Linq;
using X4GA1C_HFT_2023241.Logic;
using X4GA1C_HFT_2023241.Models;
using X4GA1C_HFT_2023241.Repository;
using X4GA1C_HFT_2023241.Repository.Repositories;

namespace X4GA1C_HFT_2023241.Client
{
    internal class Program
    {
        static LaptopWebShopDbContext database;

        static LaptopLogic laptopLogic;
        static BrandLogic brandLogic;
        static OrderLogic orderLogic;
        static OrdererLogic ordererLogic;
        static void Main(string[] args)
        {
            //just for now project reference added (repository and Logic) --> this will be susbended after API
            // for testing the database:


            database = new LaptopWebShopDbContext();

            var brandRepo = new BrandRepository(database);
            var laptopRepo = new LaptopRepository(database);
            var ordererRepo = new OrdererRepository(database);
            var orderRepo = new OrderRepository(database);

            brandLogic = new BrandLogic(brandRepo);
            laptopLogic = new LaptopLogic(laptopRepo);
            orderLogic = new OrderLogic(orderRepo);
            ordererLogic = new OrdererLogic(ordererRepo);

            //ezeket majd kitörlöm csak teszt:

             //var temp1 = orderLogic.GetOrdersByYearByMonth(2023);
             var temp2 = laptopLogic.AvgPriceByBrands();
             var temp3 = orderLogic.MostPopularBrand();

             var temp4 = orderLogic.MostPayingOrderers();

             var temp5 = orderLogic.GetStatByYear(2023);

            ;
            
            //eddig bezárólag


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


            var menu = new ConsoleMenu(args, level: 0)
                .Add("Brands", () => brandSubMenu.Show())
                .Add("Laptops", () => laptopSubMenu.Show())
                .Add("Orders", () => orderSubMenu.Show())
                .Add("Orderers", () => ordererSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();

        }

        private static void Update(string str)
        {
            List(str);

            if (str == "Laptop")
            {
                Console.WriteLine($"Enter {str} Id to Update: ");
                int result = int.Parse(Console.ReadLine());

                Laptop temp = laptopLogic.Read(result);

                foreach (var prop in temp.GetType().GetProperties() )
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

                laptopLogic.Update(temp);
            }
            else if (str == "Brand")
            {
                Console.WriteLine($"Enter {str} Id to Update: ");
                int result = int.Parse(Console.ReadLine());

                Brand temp = brandLogic.Read(result);

                foreach (var prop in temp.GetType().GetProperties() )
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

                brandLogic.Update(temp);
            }
            else if (str == "Order")
            {
                Console.WriteLine($"Enter {str} Id to Update: ");
                int result = int.Parse(Console.ReadLine());

                Order temp = orderLogic.Read(result);

                foreach(var prop in temp.GetType().GetProperties())
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

                orderLogic.Update(temp);
            }
            else if (str == "Orderer")
            {
                Console.WriteLine($"Enter {str} Id to Update: ");
                int result = int.Parse(Console.ReadLine());

                Orderer temp = ordererLogic.Read(result);

                foreach(var prop in temp.GetType().GetProperties())
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

                ordererLogic.Update(temp);
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

                laptopLogic.Delete(result);
            }
            else if (str == "Brand")
            {
                Console.WriteLine($"Enter {str} Id to Delete: ");

                int result = int.Parse(Console.ReadLine());

                brandLogic.Delete(result);
            }
            else if (str == "Order")
            {
                Console.WriteLine($"Enter {str} Id to Delete: ");

                int result = int.Parse(Console.ReadLine());

                orderLogic.Delete(result);
            }
            else if (str == "Orderer")
            {
                Console.WriteLine($"Enter {str} Id to Delete: ");

                int result = int.Parse(Console.ReadLine());

                ordererLogic.Delete(result);
            }



            Console.ReadLine();
        }

        private static void List(string str)
        {
            IEnumerable temp = null;

            if (str == "Laptop")
            {
                temp = laptopLogic.ReadAll().ToList();
            }
            else if(str == "Brand")
            {
                temp = brandLogic.ReadAll();
            }
            else if (str == "Order")
            {
                temp = orderLogic.ReadAll();
            }
            else if (str == "Orderer")
            {
                temp = ordererLogic.ReadAll();
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
            Console.WriteLine("Create a new " + str +" :");

            if (str == "Laptop")
            {
                Laptop newElement = new Laptop();

                foreach (var prop in newElement.GetType().GetProperties() )
                {
                    if (prop.Name == "ModelName")
                    {
                        Console.WriteLine(prop.Name + " :");
                        string answer = Console.ReadLine();
                        prop.SetValue(newElement, answer);
                    }
                    else if(prop.Name == "Processor")
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

                laptopLogic.Create(newElement);
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

                brandLogic.Create(newElement);
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


                orderLogic.Create(newElement);

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


                ordererLogic.Create(newElement);

            }
        }
    }
}
