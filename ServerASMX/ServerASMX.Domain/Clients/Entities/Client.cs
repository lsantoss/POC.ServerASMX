using ServerASMX.Domain.Clients.Enums;
using System;

namespace ServerASMX.Domain.Clients.Entities
{
    public class Client
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

        public Client(string name, DateTime birth, int age, EGender gender, decimal cashBalance)
        {
            SetId(0);
            SetName(name);
            SetBirth(birth);
            SetAge(age);
            SetGender(gender);
            SetCashBalance(cashBalance);
            SetActive(true);
            SetCreationDate(DateTime.Now);
            SetChangeDate(null);
        }

        public Client(long id, string name, DateTime birth, int age, EGender gender, decimal cashBalance, bool active, DateTime creationDate, DateTime? changeDate = null)
        {
            SetId(id);
            SetName(name);
            SetBirth(birth);
            SetAge(age);
            SetGender(gender);
            SetCashBalance(cashBalance);
            SetActive(active);
            SetCreationDate(creationDate);
            SetChangeDate(changeDate);
        }

        public void SetId(long id) => Id = id;
        public void SetName(string name) => Name = name;
        public void SetBirth(DateTime birth) => Birth = birth;
        public void SetAge(int age) => Age = age;
        public void SetGender(EGender gender) => Gender = gender;
        public void SetCashBalance(decimal cashBalance) => CashBalance = cashBalance;
        public void SetActive(bool active) => Active = active;
        public void SetCreationDate(DateTime creationDate) => CreationDate = creationDate;
        public void SetChangeDate(DateTime? changeDate) => ChangeDate = changeDate;

        public void Activate()
        {
            SetActive(true);
            SetCreationDate(DateTime.Now);
        }

        public void Block()
        {
            SetActive(false);
            SetCreationDate(DateTime.Now);
        }
    }
}