using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        int Incremment(ShoppingCart shoopingcart, int count);
        int Decremment(ShoppingCart shoopingcart, int count);

    }
}
