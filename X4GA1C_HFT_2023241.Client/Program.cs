using ConsoleTools;
using System;
using System.Linq;
using X4GA1C_HFT_2023241.Logic;
using X4GA1C_HFT_2023241.Models;
using X4GA1C_HFT_2023241.Repository;
using X4GA1C_HFT_2023241.Repository.Repositories;

namespace X4GA1C_HFT_2023241.Client
{
    internal class Program
    {
        static LaptopLogic laptopLogic;
        static BrandLogic brandLogic;
        static OrderLogic orderLogic;
        static OrdererLogic ordererLogic;
        static void Main(string[] args)
        {
            //just for now project reference added (repository and Logic) --> this will be susbended after API
            // for testing the database:

            LaptopWebShopDbContext database =
                new LaptopWebShopDbContext();

            var brandRepo = new BrandRepository(database);
            var laptopRepo = new LaptopRepository(database);
            var ordererRepo = new OrdererRepository(database);
            var orderRepo = new OrderRepository(database);

            brandLogic = new BrandLogic(brandRepo);
            laptopLogic = new LaptopLogic(laptopRepo);
            orderLogic = new OrderLogic(orderRepo);
            ordererLogic = new OrdererLogic(ordererRepo);


            var ordererSubMenu = new ConsoleMenu(args, level: 1)
               .Add("List", () => List("Actor"))
               .Add("Create", () => Create("Actor"))
               .Add("Delete", () => Delete("Actor"))
               .Add("Update", () => Update("Actor"))
               .Add("Exit", ConsoleMenu.Close);

            var orderSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Role"))
                .Add("Create", () => Create("Role"))
                .Add("Delete", () => Delete("Role"))
                .Add("Update", () => Update("Role"))
                .Add("Exit", ConsoleMenu.Close);

            var brandSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Director"))
                .Add("Create", () => Create("Director"))
                .Add("Delete", () => Delete("Director"))
                .Add("Update", () => Update("Director"))
                .Add("Exit", ConsoleMenu.Close);

            var laptopSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Movie"))
                .Add("Create", () => Create("Movie"))
                .Add("Delete", () => Delete("Movie"))
                .Add("Update", () => Update("Movie"))
                .Add("Exit", ConsoleMenu.Close);


            var menu = new ConsoleMenu(args, level: 0)
                .Add("Brands", () => brandSubMenu.Show())
                .Add("Laptops", () => laptopSubMenu.Show())
                .Add("Orders", () => orderSubMenu.Show())
                .Add("Orderers", () => ordererSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();

        }

        private static void Update(string v)
        {
            throw new NotImplementedException();
        }

        private static void Delete(string v)
        {
            throw new NotImplementedException();
        }

        private static void List(string v)
        {
            throw new NotImplementedException();
        }

        private static void Create(string v)
        {
            throw new NotImplementedException();
        }
    }
}
