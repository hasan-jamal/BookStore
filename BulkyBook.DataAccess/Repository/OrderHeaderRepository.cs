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
    public class OrderHeaderRepository : Repository<OrderHeader> , IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;
       
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader orderHeader)
        {
             _db.OrderHeaders.Update(orderHeader);
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
           var orderFromDB   =  _db.OrderHeaders.FirstOrDefault(u =>u.Id == id);
            if (orderFromDB != null)
            {
                orderFromDB.OrderStatus = orderStatus;
                if(paymentStatus != null)
                {
                    orderFromDB.PaymentStatus = paymentStatus;
                }
            }
        }

        public void UpdateStripPaymentId(int id, string session, string paymentItentId )
        {
            var orderFromDB = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            orderFromDB.PaymentDueDate = DateTime.Now;
            orderFromDB.PaymentIntentId = paymentItentId;
            orderFromDB.SessionId = session;
          
        }
    }
}
