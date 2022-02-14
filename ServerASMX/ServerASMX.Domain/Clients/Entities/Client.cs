using Flunt.Notifications;
using ServerASMX.Domain.Clients.Enums;
using System;

namespace ServerASMX.Domain.Clients.Entities
{
    public class Client : Notifiable
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Birth { get; private set; }
        public EGender Gender { get; private set; }
        public decimal CashBalance { get; private set; }
        public bool Active { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime? ChangeDate { get; private set; }

        public Client(string name, DateTime birth, EGender gender, decimal cashBalance)
        {
            SetId(0);
            SetName(name);
            SetBirth(birth);
            SetGender(gender);
            SetCashBalance(cashBalance);
            SetActive(true);
            SetCreationDate(DateTime.Now);
            SetChangeDate(null);
        }

        public Client(long id, string name, DateTime birth, EGender gender, decimal cashBalance, bool active, DateTime creationDate, DateTime? changeDate = null)
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

            if (Id <= 0)
                AddNotification("Id", "Id must be greater than zero");
        }

        public void SetName(string name)
        {
            Name = name;

            if (string.IsNullOrWhiteSpace(Name))
                AddNotification("Name", "Name is a required field");
            else if (Name.Length > 100)
                AddNotification("Name", $"Name must contain a maximum of 100 characters");
        }

        public void SetBirth(DateTime birth)
        {
            Birth = birth;

            if (Birth > DateTime.Now)
                AddNotification("Birth", "Birthday must be less than the current date");
        }

        public void SetGender(EGender gender)
        {
            Gender = gender;

            if (!Enum.IsDefined(typeof(EGender), Gender))
                AddNotification("Gender", "Invalid entered gender");
        }

        public void SetCashBalance(decimal cashBalance)
        {
            CashBalance = cashBalance;

            if (CashBalance < 0)
                AddNotification("CashBalance", "CashBalance must be greater than or equal to zero");
        }

        public void Activate()
        {
            SetActive(true);
            SetChangeDate(DateTime.Now);
        }

        public void Block()
        {
            SetActive(false);
            SetChangeDate(DateTime.Now);
        }

        private void SetActive(bool active) => Active = active;
        private void SetCreationDate(DateTime creationDate) => CreationDate = creationDate;
        private void SetChangeDate(DateTime? changeDate) => ChangeDate = changeDate;
    }
}