namespace Repository.Query;

public class TownQuery
{
    public const string AllTownsQuery = @"SELECT * FROM Towns";
    public const string AllProvinceTownsQuery = @"SELECT * FROM Towns WHERE ProvinceId=@Id";
    public const string TownByIdQuery = @"SELECT * FROM Towns WHERE Id=@Id";
    public const string TownByNameQuery = @"SELECT * FROM Towns WHERE Description=@Description";
    public const string InsertTownQuery = @"INSERT INTO Towns (ProvinceId, Description) 
                                                OUTPUT inserted.Id 
                                                VALUES (@ProvinceId, @Description)";
    public const string DeleteTownQuery = @"DELETE FROM Towns WHERE Id = @Id";
    public const string UpdateTownQuery = @"UPDATE Towns SET Description=@Description, ProvinceId=@ProvinceId WHERE Id=@Id";
}