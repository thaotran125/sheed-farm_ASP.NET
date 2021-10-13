using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CayGiong.Models;
using CayGiong.ViewModel;

namespace CayGiong.Dao
{
    public class DaoOrder
    {
        public List<OrderProduct> loadAddOrder()
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                return context.OrderProducts.Include("Status_Order").ToList();
            }
        }

        public OrderProduct GetOrderProduct(int id)
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                return context.OrderProducts.Where(x => x.id_Order == id).SingleOrDefault();
            }
        }

        public List<Order_detal> getOrderDetail(int id)
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                return context.Order_detal.Include("Product").Where(x => x.id_Order == id).ToList();
            }
        }

        public List<Status_Order> loadStatusOrder()
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                return context.Status_Order.ToList();
            }
        }

        //public void EditOrder(int id, int ids)
        //{
        //    using(var context=new DB_SEED_FARMEntities())
        //    {
        //        OrderProduct o = context.OrderProducts.Where(x => x.id_Order == id).SingleOrDefault();
        //        o.id_Status = ids;
        //        context.SaveChanges();
        //    }
        //}

        public int EditOrder(OrderProduct order)
        {
            using (var context = new DB_SEED_FARMEntities())
            {
                OrderProduct o = context.OrderProducts.Where(x => x.id_Order == order.id_Order).SingleOrDefault();
                if (o.id_Status == order.id_Status)
                {
                    return 0;
                }
                else
                {
                    o.id_Status = order.id_Status;
                    context.SaveChanges();
                    return 1;
                }
            }
        }
    }
}