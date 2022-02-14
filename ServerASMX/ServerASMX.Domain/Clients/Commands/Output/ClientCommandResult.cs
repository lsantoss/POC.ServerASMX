using ServerASMX.Domain.Clients.Enums;
using System;

namespace ServerASMX.Domain.Clients.Commands.Output
{
    public class ClientCommandResult
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Birth { get; private set; }
        public int Age { get; private set; }
        public EGender Gender { get; private set; }
        public decimal CashBalance { get; private set; }
        public bool Active { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? ChangeDate { get; private set; }

        public ClientCommandResult(long id, string name, DateTime birth, int age, EGender gender, decimal cashBalance, bool active, DateTime creationDate, DateTime? changeDate)
        {
            Id = id;
            Name = name;
            Birth = birth;
            Age = age;
            Gender = gender;
            CashBalance = cashBalance;
            Active = active;
            CreationDate = creationDate;
            ChangeDate = changeDate;
        }
    }
}