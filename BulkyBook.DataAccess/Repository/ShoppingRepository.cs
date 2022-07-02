using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ShoppingRepository : Repository<ShoppingCart> , IShoppingCartRepository
    {
        private readonly ApplicationDbContext _db;
       
        public ShoppingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public int Decremment(ShoppingCart shoopingcart, int count)
        {
            shoopingcart.Count -= count;
            return shoopingcart.Count;
        }

        public int Incremment(ShoppingCart shoopingcart, int count)
        {
            shoopingcart.Count += count;
            return shoopingcart.Count;
        }
    }
}
