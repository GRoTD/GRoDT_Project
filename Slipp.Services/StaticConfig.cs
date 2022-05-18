namespace Slipp.Services;

public static class StaticConfig
{
    public static string AdminRole = "Admin";
    public static string AppUserRole = "AppUser";
    public static string ClubRole = "Club";
    public static string CompanyRole = "Company";

    public static List<string> Roles = new List<string> {AdminRole, AppUserRole, ClubRole, CompanyRole};
}