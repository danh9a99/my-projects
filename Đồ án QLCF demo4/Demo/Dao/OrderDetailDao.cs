using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Dao
{
    public class OrderDetailDao
    {

        webQLCFCode db = null;
        public OrderDetailDao()
        {
            db = new webQLCFCode();
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
    }
}
