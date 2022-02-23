using ServerASMX.Domain.Core.Commands.Interfaces;
using ServerASMX.Domain.Core.Notifications;
using ServerASMX.Domain.Customers.Entities;
using ServerASMX.Domain.Customers.Enums;
using System;

namespace ServerASMX.Domain.Customers.Commands.Input
{
    public class CustomerAddCommand : IStandardCommand
    {
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public EGender Gender { get; set; }
        public decimal CashBalance { get; set; }

        public Notifier IsValid()
        {
            var notifier = new Notifier();

            if (string.IsNullOrWhiteSpace(Name))
                notifier.AddNotification("Name", "Name is a required field");
            else if (Name.Length > 100)
                notifier.AddNotification("Name", $"Name must contain a maximum of 100 characters");

            if (Birth == DateTime.MinValue)
                notifier.AddNotification("Birth", "Birth must contain a valid date");
            else if (Birth > DateTime.Now)
                notifier.AddNotification("Birth", "Birth must be less than the current date");

            if (!Enum.IsDefined(typeof(EGender), Gender))
                notifier.AddNotification("Gender", "Invalid entered gender");

            if (CashBalance < 0)
                notifier.AddNotification("CashBalance", "CashBalance must be greater than or equal to zero");

            return notifier;
        }

        public Customer MapToCustomer() => new Customer(Name, Birth, Gender, CashBalance);
    }
}