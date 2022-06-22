using POC.ServerASMX.Domain.Customers.Enums;
using System;

namespace POC.ServerASMX.Domain.Customers.Queries.Results
{
    public class CustomerQueryResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Birth { get; set; }
        public EGender Gender { get; set; }
        public decimal CashBalance { get; set; }
        public bool Active { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ChangeDate { get; set; }
    }
}