namespace Repository.Query;

public static class PostTypeQuery
{
    public const string AllPostTypesQuery = @"SELECT * FROM PostTypes";
    public const string PostTypeByIdQuery = @"SELECT * FROM PostTypes WHERE Id=@Id";
    public const string InsertPostTypeQuery = @"INSERT INTO PostTypes (Description, Prefix) 
                                                    OUTPUT inserted.Id 
                                                    VALUES (@Description, @Prefix)";

    public const string PostTypeIdByPrefixQuery = @"SELECT Id FROM PostTypes WHERE Prefix=@Prefix";
}