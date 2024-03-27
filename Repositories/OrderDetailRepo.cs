﻿using Microsoft.EntityFrameworkCore;
using Sips.SipsModels;
using Sips.ViewModels;

namespace Sips.Repositories
{
    public class OrderDetailRepo
    {
        private readonly SipsdatabaseContext _db;

        public OrderDetailRepo(SipsdatabaseContext db)
        {
            this._db = db;
        }
        public IEnumerable<OrderDetailVM> GetAll()
        {
            //var orders = _db.OrderDetails.ToList();
            var transactions = _db.Transactions
                .Include(c => c.User)
                .Include(p => p.OrderDetails)
                .Include(p => p.Store)
                .ToList();
            List<OrderDetailVM> OrdersDetailVM = new List<OrderDetailVM>();


            foreach (var transaction in transactions)
            {

                OrderDetailVM orderVM = new OrderDetailVM()
                {
                    OrderDetailIds = transaction.OrderDetails.Select(od => od.OrderDetailId).ToList(),

                    TransactionId = transaction.TransactionId,
                    DateOrdered = transaction.DateOrdered,
                    StoreId = transaction.StoreId,
                    UserId = transaction.UserId,
                    //StatusId = transaction.StatusId,
                    UserEmail = transaction.User.Email,
                    totalPrice = 0,
                    totalQuantity = 0,
                    ItemTypes = new List<string>() // Initialize ItemTypes as an empty list

                    //totalPrice = transaction.OrderDetails.price;
                };
                foreach(var orderID in orderVM.OrderDetailIds)
                {
                    var order = _db.OrderDetails.Include(m => m.MilkChoice).Include(c => c.Item).Where(id => id.OrderDetailId == orderID).FirstOrDefault();
                    //var addInDetails = order.AddInOrderDetails;
                    var milkPrice = order.MilkChoice.PriceModifier;
                    var price = order?.Price + milkPrice;

                    var quantity = order?.Quantity;
                    orderVM.totalQuantity += quantity;
                    orderVM.totalPrice += price;

                    var itemType = order?.Item.Name;
                    orderVM.ItemTypes.Add(itemType);

                }

                OrdersDetailVM.Add(orderVM);
            }

            return OrdersDetailVM;
        }



        public OrderDetailVM GetById(string id)
        {

            //List<string>? ItemTypes = null;

            var transaction = _db.Transactions
                            .Include(c => c.User)
                            .Include(p => p.OrderDetails)
                            .Include(p => p.Store)
                            .FirstOrDefault(p => p.TransactionId == id);

            var orderVM = new OrderDetailVM
            {
                OrderDetailIds = transaction?.OrderDetails.Select(od => od.OrderDetailId).ToList(),

                TransactionId = transaction?.TransactionId,
                DateOrdered = transaction?.DateOrdered,
                StoreId = transaction?.StoreId,
                UserId = transaction?.UserId,
                //StatusId = transaction.StatusId,
                UserEmail = transaction?.User.Email,
                totalPrice = 0,
                totalQuantity = 0,
                ItemTypes = new List<string>() // Initialize ItemTypes as an empty list

            };


            foreach (var orderID in orderVM?.OrderDetailIds)
            {
                var order = _db.OrderDetails.Include(m => m.MilkChoice).Include(c => c.Item).Where(id => id.OrderDetailId == orderID).FirstOrDefault();
                //var orderId = order.OrderDetailId;
                //var addInDetail = _db.AddInOrderDetails.ToList();//FirstOrDefault(ad => ad.OrderDetailId == orderID);
                //var addinPrice = addInDetail.AddIn.PriceModifier;

                //var addInDetails = order.AddInOrderDetails;

                var milkPrice = order.MilkChoice.PriceModifier;
                var price = order?.Price + milkPrice;

                var quantity = order?.Quantity;
                orderVM.totalQuantity += quantity;
                orderVM.totalPrice += price;

                var itemType = order?.Item.Name;
                orderVM.ItemTypes.Add(itemType);

            }

            return orderVM;
        }


        public string Delete(OrderDetailVM orderVM)
        {
            string message = string.Empty;
            Transaction transaction = _db.Transactions.Include(c => c.User).FirstOrDefault(c => c.TransactionId == orderVM.TransactionId);
            List<OrderDetail> orderDetails = _db.OrderDetails.Where(c => c.TransactionId == orderVM.TransactionId).ToList();


            try
            {
                foreach (OrderDetail order in orderDetails)
                {
                    _db.OrderDetails.Remove(order);
                    _db.SaveChanges();

                }

                message = $"Transaction with Id:{transaction?.TransactionId} deleted successfully";

                _db.Transactions.Remove(transaction);
                _db.SaveChanges();

            }
            catch (Exception e)
            {
                message = $"Error deleting transaction-{transaction?.TransactionId}: {e.Message}";
            }
            return message;
        }

    }
}
