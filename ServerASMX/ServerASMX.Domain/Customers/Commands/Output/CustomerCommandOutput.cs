using ServerASMX.Domain.Customers.Enums;
using System;

namespace ServerASMX.Domain.Customers.Commands.Output
{
    public class CustomerCommandOutput
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public EGender Gender { get; set; }
        public decimal CashBalance { get; set; }
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ChangeDate { get; set; }

        public CustomerCommandOutput() { }

        public CustomerCommandOutput(long id, string name, DateTime birth, EGender gender, decimal cashBalance, bool active, DateTime creationDate, DateTime? changeDate)
        {
            Id = id;
            Name = name;
            Birth = birth;
            Gender = gender;
            CashBalance = cashBalance;
            Active = active;
            CreationDate = creationDate;
            ChangeDate = changeDate;
        }
    }
}