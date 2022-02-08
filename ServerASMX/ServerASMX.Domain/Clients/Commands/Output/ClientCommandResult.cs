using ServerASMX.Domain.Clients.Enums;
using System;

namespace ServerASMX.Domain.Clients.Commands.Output
{
    public class ClientCommandResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public int Age { get; set; }
        public EGender Gender { get; set; }
        public decimal CashBalance { get; set; }
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ChangeDate { get; set; }
    }
}