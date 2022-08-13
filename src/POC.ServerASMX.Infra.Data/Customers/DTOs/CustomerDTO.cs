using POC.ServerASMX.Infra.Enums;
using System;

namespace POC.ServerASMX.Infra.Data.Customers.DTOs
{
    public class CustomerDTO
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Birth { get; private set; }
        public EGender Gender { get; private set; }
        public decimal CashBalance { get; private set; }
        public bool Active { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? ChangeDate { get; private set; }

        public CustomerDTO() { }

        public CustomerDTO(long id, string name, DateTime birth, EGender gender, decimal cashBalance, bool active, DateTime creationDate, DateTime? chanceDate = null)
        {
            Id = id;
            Name = name;
            Birth = birth;
            Gender = gender;
            CashBalance = cashBalance;
            Active = active;
            CreationDate = creationDate;
            ChangeDate = chanceDate;
        }
    }
}
