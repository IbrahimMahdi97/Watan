namespace Repository.Query;

public class ProvinceQuery
{
    public const string AllProvincesQuery = @"SELECT * FROM Provinces";
    public const string ProvinceByIdQuery = @"SELECT * FROM Provinces WHERE Id=@Id";
    public const string ProvinceByNameQuery = @"SELECT * FROM Provinces WHERE Description=@Description";
    public const string InsertProvinceQuery = @"INSERT INTO Provinces (Description) 
                                                OUTPUT inserted.Id 
                                                VALUES (@Description)";
    public const string DeleteProvinceQuery = @"DELETE FROM Provinces WHERE Id = @Id";
    public const string UpdateProvinceQuery = @"UPDATE Provinces SET Description=@Description WHERE Id=@Id";
}