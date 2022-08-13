using System.ComponentModel;

namespace POC.ServerASMX.Infra.Enums
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