﻿using POC.ServerASMX.Infra.Enums;
using System;

namespace POC.ServerASMX.Domain.Customers.Commands.Result
{
    public class CustomerCommandResult
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Birth { get; private set; }
        public EGender Gender { get; private set; }
        public decimal CashBalance { get; private set; }
        public bool Active { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? ChangeDate { get; private set; }

        public CustomerCommandResult() { }

        public CustomerCommandResult(long id, string name, DateTime birth, EGender gender, decimal cashBalance, bool active, DateTime creationDate, DateTime? changeDate = null)
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