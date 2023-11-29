namespace Repository.Query;

public static class EventAttendanceQuery
{
    public const string InsertQuery = @"INSERT INTO EventAttendance (UserId, PostId) 
                                                        VALUES (@UserId, @PostId)";

    public const string AttendeesDetailQuery = @"SELECT EA.Count(*) AS Count, 
                                                US.FullName, US.MotherName, US.ProvinceOfBirth, US.Gender, US.DateOfBirth,
                                                US.District, US.StreetNumber, US.HouseNumber, US.NationalIdNumber, US.ResidenceCardNumber,
                                                 US.VoterCardNumber, US.PhoneNumber, US.Email 
                                                FROM EventAttendance EA LEFT OUTER JOIN Users US ON (EA.UserId = US.Id) 
                                                WHERE PostId=@PostId ";
}