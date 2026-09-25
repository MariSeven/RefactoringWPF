using RefactoringWPF.Services;
using RefactoringWPF.Utils;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace RefactoringWPF.Models
{
    internal class MainViewModel : INotifyPropertyChanged

    {
        private readonly IOrderService _service;

        public Action AddOrderAction { get; set; }
        public Action RefreshAction { get; set; }
        public Action DeleteOrderAction { get; set; }
        public Action ShowDiscountAction { get; set; }

        public MainViewModel(IOrderService service)
        {
            _service = service;
        }

        public ObservableCollection<Order> Orders => _service.Orders;

        public string TotalText =>
            "Итого " + Helper.FormatMoney(_service.GetTotalRevenue());

        public bool TryAddOrder(string customer, string product, string qtyText, string priceText)
        {
            if (!int.TryParse(qtyText, out int qty))
            {
                MessageBox.Show("Количество должно быть числом");
                return false;
            }

            if (!decimal.TryParse(priceText, out decimal price))
            {
                MessageBox.Show("Цена должна быть числом");
                return false;
            }

            if (!Helper.IsValidEmail(customer))
            {
                MessageBox.Show("неверный Email");
                return false;
            }

            _service.AddOrder(customer, product, qty, price);
            return true;
        }

        public bool TryDeleteOrder(Order selected)
        {
            if (selected == null)
            {
                MessageBox.Show("Выберите заказ!");
                return false;
            }

            _service.DeleteOrder(selected.Id);
            return true;
        }

        public void ShowDiscountFor(Order selected)
        {
            if (selected == null)
            {
                MessageBox.Show("Выберите заказ!");
                return;
            }

            string discount = _service.GetDiscount(selected.Quantity, selected.GetTotal());
            MessageBox.Show("Скидка " + discount);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public void NotifyRefresh() => OnPropertyChanged(nameof(TotalText));


    }
}
