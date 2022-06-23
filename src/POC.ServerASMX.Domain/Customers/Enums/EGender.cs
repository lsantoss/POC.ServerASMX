using System.ComponentModel;

namespace POC.ServerASMX.Domain.Customers.Enums
{
    public enum EGender
    {
        [Description("Male")]
        Male = 0,

        [Description("Female")]
        Female = 1,

        [Description("Other")]
        Other = 2
    }
}