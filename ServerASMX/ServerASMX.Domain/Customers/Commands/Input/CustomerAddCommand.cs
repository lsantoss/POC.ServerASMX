using ServerASMX.Domain.Core.Commands.Interfaces;
using ServerASMX.Domain.Core.Notifications;
using ServerASMX.Domain.Customers.Entities;
using ServerASMX.Domain.Customers.Enums;
using System;

namespace ServerASMX.Domain.Customers.Commands.Input
{
    public class CustomerAddCommand : Notifier, IStandardCommand
    {
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public EGender Gender { get; set; }
        public decimal CashBalance { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Name))
                AddNotification("Name", "Name is a required field");
            else if (Name.Length > 100)
                AddNotification("Name", $"Name must contain a maximum of 100 characters");

            if (Birth == DateTime.MinValue)
                AddNotification("Birth", "Birth must contain a valid date");
            else if (Birth > DateTime.Now)
                AddNotification("Birth", "Birth must be less than the current date");

            if (!Enum.IsDefined(typeof(EGender), Gender))
                AddNotification("Gender", "Invalid entered gender");

            if (CashBalance < 0)
                AddNotification("CashBalance", "CashBalance must be greater than or equal to zero");

            return Valid;
        }

        public Customer MapToCustomer() => new Customer(Name, Birth, Gender, CashBalance);
    }
}