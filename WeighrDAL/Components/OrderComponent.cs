using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeighrDAL.Models;

namespace WeighrDAL.Components
{
    public class OrderComponent
    {
        public void AddOrder(Order order)
        {
            using (var db = new WeighrContext())
            {
                db.Orders.Add(order);
                db.SaveChanges();

            }
        }
        public void UpdateOrder(Order order)
        {
            using (var db = new WeighrContext())
            {
                db.Orders.Update(order);
                db.SaveChanges();
            }
        }

        public Order GetOrder(int orderId)
        {
            using (var db = new WeighrContext())
            {
                return db.Orders
                    .Where(p => p.OrderId == orderId).FirstOrDefault();

            }
        }
        public List<Order> GetOrders()
        {
            using (var db = new WeighrContext())
            {
                return db.Orders.ToList();

            }
        }


        ////ORDER DETAILS METHODS
        public void AddOrderDetail(OrderDetail orderDetail)
        {
            using (var db = new WeighrContext())
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();

            }
        }
        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            using (var db = new WeighrContext())
            {
                db.OrderDetails.Update(orderDetail);
                db.SaveChanges();
            }
        }

        public OrderDetail GetOrderDetail(int orderDetailId)
        {
            using (var db = new WeighrContext())
            {
                return db.OrderDetails
                    .Where(p => p.OrderDetailId == orderDetailId).FirstOrDefault();

            }
        }
        public List<OrderDetail> GetOrderDetails()
        {
            using (var db = new WeighrContext())
            {
                return db.OrderDetails.ToList();

            }
        }

    }
}
