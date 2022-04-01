namespace ServerASMX.Test.Base.Constants
{
    internal class ConnectionStrings
    {
        public static string DataBase { get; } = @"Data Source=SANTOS-PC\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=ServerASMX";
        public static string DataBaseTest { get; } = @"Data Source=SANTOS-PC\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=ServerASMXTest";
    }
}