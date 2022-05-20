namespace POC.ServerASMX.Domain.Customers.Repositories.Queries
{
    public static class CustomerQueries
    {
        public static string Insert { get; } = @"INSERT INTO Customer 
                                                    (Name,
                                                     Birth,
                                                     Gender,
                                                     CashBalance,
                                                     Active,
                                                     CreationDate,
                                                     ChangeDate) 
                                                 VALUES 
                                                    (@Name,
                                                     @Birth,
                                                     @Gender,
                                                     @CashBalance,
                                                     @Active,
                                                     @CreationDate,
                                                     @ChangeDate) ; 
                                                 SELECT SCOPE_IDENTITY();";

        public static string Update { get; } = @"UPDATE Customer SET
                                                    Name = @Name,
                                                    Birth = @Birth,
                                                    Gender = @Gender,
                                                    CashBalance = @CashBalance,
                                                    Active = @Active,
                                                    CreationDate = @CreationDate,
                                                    ChangeDate = @ChangeDate 
                                                 WHERE Id = @Id";

        public static string Delete { get; } = @"DELETE FROM Customer WHERE Id = @Id";

        public static string ChangeActivityState { get; } = @"UPDATE Customer SET
                                                                Active = @Active,
                                                                ChangeDate = @ChangeDate 
                                                              WHERE Id = @Id";

        public static string Get { get; } = @"SELECT 
                                                Id AS Id,
                                                Name AS Name,
                                                Birth AS Birth,
                                                Gender AS Gender,
                                                CashBalance AS CashBalance,
                                                Active AS Active,
                                                CreationDate AS CreationDate,
                                                ChangeDate AS ChangeDate 
                                              FROM Customer WITH(NOLOCK)
                                              WHERE Id = @Id";

        public static string List { get; } = @"SELECT 
                                                 Id AS Id,
                                                 Name AS Name,
                                                 Birth AS Birth,
                                                 Gender AS Gender,
                                                 CashBalance AS CashBalance,
                                                 Active AS Active,
                                                 CreationDate AS CreationDate,
                                                 ChangeDate AS ChangeDate 
                                               FROM Customer WITH(NOLOCK)
                                               ORDER BY Id";

        public static string CheckId { get; } = @"SELECT Id FROM Customer WITH(NOLOCK) WHERE Id = @Id";
    }
}