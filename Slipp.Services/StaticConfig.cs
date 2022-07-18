namespace Slipp.Services;

public static class StaticConfig
{
    public const string AdminRole = "Admin";
    public const string AppUserRole = "AppUser";
    public const string ClubRole = "Club";
    public const string CompanyRole = "Company";

    public static List<string> Roles = new List<string> {AdminRole, AppUserRole, ClubRole, CompanyRole};
}