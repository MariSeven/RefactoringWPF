using RefactoringWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RefactoringWPF.Services
{
    public interface IOrderService
    {
        ObservableCollection<Order> Orders { get; }
        void AddOrder(string customer, string product, int qty, decimal price);
        decimal GetTotalRevenue();
        decimal GetTotalRevenueByStatus(OrderStatus status);
        List<Order> GetOrdersByStatus(OrderStatus status);
        void UpdateStatusWithLog(int id, OrderStatus newStatus);
        void UpdateStatusSilent(int id, OrderStatus newStatus);
        Order? FindOrder(int id);
        string GetDiscount(int qty, decimal total);
        void DeleteOrder(int id);
    }
}
