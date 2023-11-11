namespace Repository.Query;

public static class RoleQuery
{
    public const string UserRolesByIdQuery =
        @"SELECT R.Id, R.Description FROM Roles R
                JOIN UserRoles UR on R.Id = UR.RoleId
                WHERE UR.UserId = @id";
}