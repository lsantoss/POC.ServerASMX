using POC.ServerASMX.Domain.Customers.Validations;
using POC.ServerASMX.Infra.Commands.Interfaces;
using POC.ServerASMX.Infra.Enums;
using POC.ServerASMX.Infra.Notifications;
using System;

namespace POC.ServerASMX.Domain.Customers.Commands.Input
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