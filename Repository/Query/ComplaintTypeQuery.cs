namespace Repository.Query;

public static class ComplaintTypeQuery
{
    public const string AllComplaintTypesQuery = @"SELECT * FROM ComplaintTypes";
    public const string ComplaintTypeByIdQuery = @"SELECT * FROM ComplaintTypes WHERE Id=@Id";
    public const string InsertComplaintTypeQuery = @"INSERT INTO ComplaintTypes (Description, Prefix) 
                                                    OUTPUT inserted.Id 
                                                    VALUES (@Description, @Prefix)";

    public const string ComplaintTypeIdByPrefixQuery = @"SELECT Id FROM ComplaintTypes WHERE Prefix=@Prefix";
}