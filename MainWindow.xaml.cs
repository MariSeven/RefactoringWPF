using RefactoringWPF.Models;
using RefactoringWPF.Services;
using System.Windows;
using System.Windows.Controls;

namespace RefactoringWPF
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            IOrderService service = new OrderService();
            _viewModel = new MainViewModel(service);

            dgOrders.ItemsSource = _viewModel.Orders;
            _viewModel.AddOrderAction = () =>
            {
                if (_viewModel.TryAddOrder(
                        txtCustomer.Text, txtProduct.Text,
                        txtQty.Text, txtPrice.Text))
                {
                    txtCustomer.Clear();
                    txtProduct.Clear();
                    txtQty.Clear();
                    txtPrice.Clear();
                    RefreshUI();
                }
            };

            _viewModel.RefreshAction = () => RefreshUI();

            _viewModel.DeleteOrderAction = () =>
            {
                var selected = dgOrders.SelectedItem as Order;
                if (_viewModel.TryDeleteOrder(selected))
                    RefreshUI();
            };

            _viewModel.ShowDiscountAction = () =>
            {
                var selected = dgOrders.SelectedItem as Order;
                _viewModel.ShowDiscountFor(selected);
            };
        }

        private void RefreshUI()
        {
            lblTotal.Text = _viewModel.TotalText;
            _viewModel.NotifyRefresh();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e) =>
            _viewModel.AddOrderAction?.Invoke();

        private void BtnRefresh_Click(object sender, RoutedEventArgs e) =>
            _viewModel.RefreshAction?.Invoke();

        private void BtnDelete_Click(object sender, RoutedEventArgs e) =>
            _viewModel.DeleteOrderAction?.Invoke();

        private void BtnDiscount_Click(object sender, RoutedEventArgs e) =>
            _viewModel.ShowDiscountAction?.Invoke();
    }
}