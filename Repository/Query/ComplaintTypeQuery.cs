namespace Repository.Query;

public static class ComplaintTypeQuery
{
    public const string AllComplaintTypesQuery = @"SELECT * FROM ComplaintType";
    public const string ComplaintTypeByIdQuery = @"SELECT * FROM ComplaintType WHERE Id=@Id";
    public const string InsertComplaintTypeQuery = @"INSERT INTO ComplaintType (Description, Prefix) 
                                                    OUTPUT inserted.Id 
                                                    VALUES (@Description, @Prefix)";

    public const string ComplaintTypeIdByPrefixQuery = @"SELECT Id FROM ComplaintType WHERE Prefix=@Prefix";
}