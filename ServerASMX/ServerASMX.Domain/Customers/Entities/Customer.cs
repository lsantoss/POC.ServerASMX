using ServerASMX.Domain.Core.Notifications;
using ServerASMX.Domain.Customers.Commands.Output;
using ServerASMX.Domain.Customers.Enums;
using ServerASMX.Domain.Customers.Validations;
using System;

namespace ServerASMX.Domain.Customers.Entities
{
    public class Customer : Notifier
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Birth { get; private set; }
        public EGender Gender { get; private set; }
        public decimal CashBalance { get; private set; }
        public bool Active { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? ChangeDate { get; private set; }

        public Customer(string name, DateTime birth, EGender gender, decimal cashBalance)
        {
            Id = 0;
            SetName(name);
            SetBirth(birth);
            SetGender(gender);
            SetCashBalance(cashBalance);
            SetActive(true);
            SetCreationDate(DateTime.Now);
            SetChangeDate(null);
        }

        public Customer(long id, string name, DateTime birth, EGender gender, decimal cashBalance, bool active, DateTime creationDate, DateTime? changeDate = null)
        {
            SetId(id);
            SetName(name);
            SetBirth(birth);
            SetGender(gender);
            SetCashBalance(cashBalance);
            SetActive(active);
            SetCreationDate(creationDate);
            SetChangeDate(changeDate);
        }

        public void SetId(long id)
        {
            Id = id;
            AddNotification(new CustomerValidation().ValidateId(Id));
        }

        public void SetName(string name)
        {
            Name = name;
            AddNotification(new CustomerValidation().ValidateName(Name));
        }

        public void SetBirth(DateTime birth)
        {
            Birth = birth;
            AddNotification(new CustomerValidation().ValidateBirth(Birth));
        }

        public void SetGender(EGender gender)
        {
            Gender = gender;
            AddNotification(new CustomerValidation().ValidateGender(Gender));
        }

        public void SetCashBalance(decimal cashBalance)
        {
            CashBalance = cashBalance;
            AddNotification(new CustomerValidation().ValidateCashBalance(CashBalance));
        }

        public CustomerCommandOutput MapToCustomerCommandOutput() => new CustomerCommandOutput(Id, Name, Birth, Gender, CashBalance, Active, CreationDate, ChangeDate);

        private void SetActive(bool active) => Active = active;
        private void SetCreationDate(DateTime creationDate) => CreationDate = creationDate;
        private void SetChangeDate(DateTime? changeDate) => ChangeDate = changeDate;
    }
}