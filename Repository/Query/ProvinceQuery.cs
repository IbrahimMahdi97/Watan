namespace Repository.Query;

public class ProvinceQuery
{
    public const string SelectByParametersQuery = @"SELECT P.Id, P.Description FROM Provinces P
                                                        WHERE IIF(@Id = 0, 0, P.Id) = @Id 
                                                          AND (P.Description LIKE '%' + @Description + '%' OR @Description IS NULL)";
 
    public const string ProvinceByIdQuery = @"SELECT * FROM Provinces WHERE Id=@Id";
    /*   public const string ProvinceByNameQuery = @"SELECT * FROM Provinces WHERE Description=@Description";
    */
    public const string InsertProvinceQuery = @"INSERT INTO Provinces (Description) 
                                                OUTPUT inserted.Id 
                                                VALUES (@Description)";
    public const string DeleteProvinceQuery = @"DELETE FROM Provinces WHERE Id = @Id";
    public const string UpdateProvinceQuery = @"UPDATE Provinces SET Description=@Description WHERE Id=@Id";
}