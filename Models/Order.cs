using System;
using System.ComponentModel;

namespace RefactoringWPF.Models
{
    public enum OrderStatus
    {
        New,
        Paid,
        Shipped,
        Cancelled
    }
    public class Order : INotifyPropertyChanged
    {
        
        public int Id { get; set; }
        private string _CustomerName;
        private string _ProductName;
        private int _Quantity;
        private decimal _Price;
        private OrderStatus _Status; 
        public DateTime CreatedDate { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


        public string CustomerName
        {
            get => _CustomerName;
            set { _CustomerName = value; OnPropertyChanged(nameof(CustomerName)); }
        }
        public string ProductName
        {
            get => _ProductName;
            set { _ProductName = value; OnPropertyChanged(nameof(ProductName)); }
        }
        public int Quantity
        {
            get => _Quantity;
            set { _Quantity = value; OnPropertyChanged(nameof(Quantity)); }
        }
        public decimal Price
        {
            get => _Price;
            set { _Price = value; OnPropertyChanged(nameof(Price)); }
        }
        public OrderStatus Status
        {
            get => _Status;
            set { _Status = value; OnPropertyChanged(nameof(Status)); }
        }

        public decimal GetTotal()
        {
            return Quantity * Price;
        }
    }
}
