using RefactoringWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RefactoringWPF.Services
{
    public class OrderService : IOrderService
    {
        private const int QtyThresholdHigh = 100;
        private const int QtyThresholdMedium = 50;
        private const int QtyThresholdLow = 10;
        private const decimal TotalThreshold = 10000m;


        public ObservableCollection<Order> Orders { get; } = new ObservableCollection<Order>();
        private int nextId = 1;

        public static List<Order> orders = new List<Order>();

        public void AddOrder(string customer, string product, int qty, decimal price)
        {
            var order = new Order
            {
                Id = nextId++,
                CustomerName = customer,
                ProductName = product,
                Quantity = qty,
                Price = price,
                Status = OrderStatus.New,
                CreatedDate = DateTime.Now
            };
            Orders.Add(order);
        }

        public decimal GetTotalRevenue() =>
            Orders.Sum(o => o.GetTotal());

        public decimal GetTotalRevenueByStatus(OrderStatus status) =>
            Orders.Where(o => o.Status == status).Sum(o => o.GetTotal());

        public List<Order> GetOrdersByStatus(OrderStatus status) =>
            Orders.Where(o => o.Status == status).ToList();


        public void UpdateStatusWithLog(int id, OrderStatus newStatus)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                order.Status = newStatus;
                Console.WriteLine($"Status updated for order {id} to {newStatus}");
            }
        }
        public void UpdateStatusSilent(int id, OrderStatus newStatus)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
                order.Status = newStatus;
        }

        Order? IOrderService.FindOrder(int id) =>
            Orders.FirstOrDefault(o => o.Id == id);


        public string GetDiscount(int qty, decimal total)
        {
            if (total > TotalThreshold)
                return "25%";

            var thresholds = new List<(int MinQty, string Discount)>
            {
                (QtyThresholdHigh, "20%"),
                (QtyThresholdMedium, "10%"),
                (QtyThresholdLow, "5%")
            };

            foreach (var (minQty, discount) in thresholds)
            {
                if (qty > minQty)
                    return discount;
            }

            return "0%";
        }

        public void DeleteOrder(int id)
        {
            var order = Orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
                Orders.Remove(order);
        }
    }
}