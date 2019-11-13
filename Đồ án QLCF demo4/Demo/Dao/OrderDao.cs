using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Dao
{
    public class OrderDao
    {
        webQLCFCode db = null;
        public OrderDao()
        {
            db = new webQLCFCode();
        }
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }
    }
}
