namespace Repository.Query;

public static class PostTypeQuery
{
    public const string AllPostTypesQuery = @"SELECT * FROM PostType";
    public const string PostTypeByIdQuery = @"SELECT * FROM PostType WHERE Id=@Id";
    public const string InsertPostTypeQuery = @"INSERT INTO PostType (Description, Prefix) 
                                                    OUTPUT inserted.Id 
                                                    VALUES (@Description, @Prefix)";

    public const string PostTypeIdByPrefixQuery = @"SELECT Id FROM PostType WHERE Prefix=@Prefix";
}