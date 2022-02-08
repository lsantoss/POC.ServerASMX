using ServerASMX.Domain.Clients.Enums;
using System;

namespace ServerASMX.Domain.Clients.Commands.Input
{
    public class ClientAddCommand
    {
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public int Age { get; set; }
        public EGender Gender { get; set; }
        public decimal CashBalance { get; set; }
    }
}