using RefactoringWPF.Models;
using System;
using System.Net.Mail;

namespace RefactoringWPF.Utils
{
    public static class Helper
    {
        public static string FormatMoney(decimal amount) => amount.ToString("0.00") + " руб.";

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static string StatusToRussian(OrderStatus status) => status switch
        {
            OrderStatus.New => "Новый",
            OrderStatus.Paid => "Оплачен",
            OrderStatus.Shipped => "Отправлен",
            OrderStatus.Cancelled => "Отменён",
            _ => "Неизвестно"
        };
    }
}
