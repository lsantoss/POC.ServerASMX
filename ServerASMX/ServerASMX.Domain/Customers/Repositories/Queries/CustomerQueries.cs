namespace ServerASMX.Domain.Customers.Repositories.Queries
{
    public static class CustomerQueries
    {
        public static string Insert { get; } = @"INSERT INTO Client 
                                                    (Id,
                                                     Name,
                                                     Birth,
                                                     Age,
                                                     Gender,
                                                     CashBalance,
                                                     Active,
                                                     CreationDate,
                                                     ChangeDate) 
                                                 VALUES 
                                                    (@Id,
                                                     @Name,
                                                     @Birth,
                                                     @Age,
                                                     @Gender,
                                                     @CashBalance,
                                                     @Active,
                                                     @CreationDate,
                                                     @ChangeDate) ; 
                                                 SELECT SCOPE_IDENTITY();";

        public static string Update { get; } = @"UPDATE Client SET
                                                    Name = @Name,
                                                    Birth = @Birth,
                                                    Age = @Age,
                                                    Gender = @Gender,
                                                    CashBalance = @CashBalance,
                                                    Active = @Active,
                                                    CreationDate = @CreationDate,
                                                    ChangeDate = @ChangeDate 
                                                 WHERE Id = @Id";

        public static string Delete { get; } = @"DELETE FROM Client WHERE Id = @Id";

        public static string Get { get; } = @"SELECT 
                                                Id AS Id,
                                                Name AS Name,
                                                Birth AS Birth,
                                                Age AS Age,
                                                Gender AS Gender,
                                                CashBalance AS CashBalance,
                                                Active AS Active,
                                                CreationDate AS CreationDate,
                                                ChangeDate AS ChangeDate 
                                              FROM Client WITH(NOLOCK)
                                              WHERE Id = @Id";

        public static string List { get; } = @"SELECT 
                                                 Id AS Id,
                                                 Name AS Name,
                                                 Birth AS Birth,
                                                 Age AS Age,
                                                 Gender AS Gender,
                                                 CashBalance AS CashBalance,
                                                 Active AS Active,
                                                 CreationDate AS CreationDate,
                                                 ChangeDate AS ChangeDate 
                                               FROM Client WITH(NOLOCK)
                                               ORDER BY Id";

        public static string CheckId { get; } = @"SELECT Id FROM Client WITH(NOLOCK) WHERE Id = @Id";
    }
}