using Flunt.Notifications;
using ServerASMX.Domain.Customers.Enums;
using ServerASMX.Domain.Core.Commands.Interfaces;
using System;

namespace ServerASMX.Domain.Customers.Commands.Input
{
    public class CustomerAddCommand : Notifiable, IStandardCommand
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

            if (Birth > DateTime.Now)
                AddNotification("Birth", "Birthday must be less than the current date");

            if (!Enum.IsDefined(typeof(EGender), Gender))
                AddNotification("Gender", "Invalid entered gender");

            if (CashBalance < 0)
                AddNotification("CashBalance", "CashBalance must be greater than or equal to zero");

            return Valid;
        }
    }
}