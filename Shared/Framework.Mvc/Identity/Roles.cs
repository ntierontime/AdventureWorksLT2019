namespace Framework.Mvc.Identity
{
    /// <summary>
    /// TODO: roles are hard coded, Controller->View->Authorized->Roles should be customized per view.
    /// </summary>
    public enum Roles
    {
        Member,
        Admin,
        Employee,
        SystemAdmin,
        Assistant,
        Host,
        Visitor,
    }
    public class RolesCombination
    {
        public const string AllRoles = @"Member, Admin, Employee, SystemAdmin, Assistant, Host, Visitor";
    }
}

