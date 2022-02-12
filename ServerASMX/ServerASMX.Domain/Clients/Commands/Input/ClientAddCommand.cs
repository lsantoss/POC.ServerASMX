using Flunt.Notifications;
using ServerASMX.Domain.Clients.Enums;
using ServerASMX.Domain.Core.Commands.Interfaces;
using System;

namespace ServerASMX.Domain.Clients.Commands.Input
{
    public class ClientAddCommand : Notifiable, IStandardCommand
    {
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public int Age { get; set; }
        public EGender Gender { get; set; }
        public decimal CashBalance { get; set; }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}