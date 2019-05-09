using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IPORevamp.Data.UserManagement.Model
{
    public class ApplicationRoleStore: RoleStore<ApplicationRole,IPOContext, int, ApplicationUserRole, IdentityRoleClaim<int>>
    {
        public ApplicationRoleStore(IPOContext context) : base(context)
        {
        }
    }
}
