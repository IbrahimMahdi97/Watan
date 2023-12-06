namespace Repository.Query;

public class RegionQuery
{
    public const string SelectByParametersQuery = @"SELECT R.Id, R.Description, R.TownId FROM Regions R
                                                        WHERE IIF(@Id =0, 0, R.Id) = @Id
                                                        AND IIF(@TownId =0, 0, R.TownId) = @TownId
                                                        AND (R.Description LIKE '%' + @Description+ '%' OR @Description IS NULL)";
    
    
  // public const string AllTownRegionsQuery = @"SELECT * FROM Regions WHERE TownId=@Id";
   
    public const string RegionByIdQuery = @"SELECT * FROM Regions WHERE Id=@Id";
   // public const string RegionByNameQuery = @"SELECT * FROM Regions WHERE Description=@Description";
    
    public const string InsertRegionQuery = @"INSERT INTO Regions (TownId, Description) 
                                                OUTPUT inserted.Id 
                                                VALUES (@TownId, @Description)";
    public const string DeleteRegionQuery = @"DELETE FROM Regions WHERE Id = @Id";
    public const string UpdateRegionQuery = @"UPDATE Regions SET Description=@Description, TownId=@TownId WHERE Id=@Id";
}