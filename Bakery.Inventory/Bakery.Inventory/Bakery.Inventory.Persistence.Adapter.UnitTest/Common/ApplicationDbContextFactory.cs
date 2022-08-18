using Bakery.Inventory.DomainApi.Model;
using Bakery.Inventory.Persistence.Adapter.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Bakery.Inventory.Persistence.Adapter.UnitTest.Common
{
    public static class ApplicationDbContextFactory
    {
        public static List<Bakery.Inventory.DomainApi.Model.Inventory> GetDeals()
        {
            return new List<Bakery.Inventory.DomainApi.Model.Inventory>()
            {
                new Bakery.Inventory.DomainApi.Model.Inventory(){Id=1, ProductId = 1, Quantity = 2,Invoice = "", CreatedDate = DateTime.Now, Location = "Almacen 1"},
                new Bakery.Inventory.DomainApi.Model.Inventory(){Id=2, ProductId = 2, Quantity = 3,Invoice = "", CreatedDate = DateTime.Now, Location = "Almacen 2"},
                new Bakery.Inventory.DomainApi.Model.Inventory(){Id=3, ProductId = 3, Quantity = 4,Invoice = "", CreatedDate = DateTime.Now, Location = "Almacen 3"},
            };
        }

        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();
            context.Inventories.AddRange(GetDeals());
            context.SaveChanges();
            return context;
        }
        public static void Destroy(ApplicationDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        public static Bakery.Inventory.DomainApi.Model.Inventory dummyInventory()
        {
            return new Bakery.Inventory.DomainApi.Model.Inventory
            {
                Id = 0,
                ProductId = 4,
                Quantity = 1,
                Invoice = "",
                CreatedDate = DateTime.Now,
                Location = "Almacen 1"
            };
        }
    }
}
