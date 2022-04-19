using ServerASMX.Domain.Core.Commands.Interfaces;
using ServerASMX.Domain.Core.Notifications;
using ServerASMX.Domain.Customers.Enums;
using ServerASMX.Domain.Customers.Validations;
using System;

namespace ServerASMX.Domain.Customers.Commands.Input
{
    public class CustomerUpdateCommand : Notifier, IStandardCommand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public EGender Gender { get; set; }
        public decimal CashBalance { get; set; }

        public bool IsValid()
        {
            AddNotification(new CustomerValidation().ValidateCommand(this));
            return Valid;
        }
    }
}