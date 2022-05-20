using POC.ServerASMX.Domain.Core.Commands.Interfaces;
using POC.ServerASMX.Domain.Core.Notifications;
using POC.ServerASMX.Domain.Customers.Entities;
using POC.ServerASMX.Domain.Customers.Enums;
using POC.ServerASMX.Domain.Customers.Validations;
using System;

namespace POC.ServerASMX.Domain.Customers.Commands.Input
{
    public class CustomerAddCommand : Notifier, IStandardCommand
    {
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public EGender Gender { get; set; }
        public decimal CashBalance { get; set; }

        public bool IsValid()
        {
            AddNotification(new CustomerValidation().ValidateCommand(this));
            return Valid;
        }

        public Customer MapToCustomer() => new Customer(Name, Birth, Gender, CashBalance);
    }
}