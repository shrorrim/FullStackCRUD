using System;
using System.Linq;
using X4GA1C_HFT_2023241.Models;
using X4GA1C_HFT_2023241.Repository;

namespace X4GA1C_HFT_2023241.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //just for now project reference added (repository) --> this will be susbended after API
            // for testing the database:

            LaptopWebShopDbContext database = new LaptopWebShopDbContext();

            var laptops = database.Laptops.ToList();

            var orderers = database.Orderers.ToList();

            var brands = database.Brands.ToList();

            var orders = database.Orders.ToList();



            



            Console.ReadLine();
        }
    }
}
