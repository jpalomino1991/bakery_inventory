using Bakery.Inventory.DomainApi.Port;
using Bakery.Inventory.Persistence.Adapter.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Inventory.Domain
{
    public class InventoryDomain<T> : IRequestInventory<T> where T : Bakery.Inventory.DomainApi.Model.Inventory
    {
        ApplicationDbContext _dbContext;
        private readonly DbSet<T> table;

        public InventoryDomain(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            table = _dbContext.Set<T>();
        }

        public IEnumerable<T> GetValues()
        {
            return table.ToList();
        }

        public T GetValue(int id)
        {
            return table.Find(id);
        }

        public T AddValue(T value)
        {
            var exists = table.Where(i => i.ProductId == value.ProductId).FirstOrDefault();
            if(exists != null)
                return null;
            table.Add(value);
            _dbContext.SaveChanges();
            return value;
        }

        public T DeleteValue(T value)
        {
            var exists = table.Where(i => i.ProductId == value.ProductId).FirstOrDefault();
            if (exists == null)
                return null;
            if (exists.Quantity >= value.Quantity)
                exists.Quantity -= value.Quantity;
            else
                throw new InvalidOperationException();
            table.Update(exists);
            _dbContext.SaveChanges();
            return exists;
        }

        public T EditValue(T value)
        {
            var exists = table.Where(i => i.ProductId == value.ProductId).FirstOrDefault();
            if (exists == null)
                return null;
            exists.Quantity = value.Quantity;
            table.Update(exists);
            _dbContext.SaveChanges();
            return exists;
        }
    }
}
