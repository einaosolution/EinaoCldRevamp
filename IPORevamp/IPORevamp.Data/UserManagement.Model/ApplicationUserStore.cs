using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IPORevamp.Data.UserManagement.Model
{
    public class ApplicationUserStore: UserStore<ApplicationUser, ApplicationRole,IPOContext,int,ApplicationUserClaim,ApplicationUserRole,ApplicationUserLogin,IdentityUserToken<int>,IdentityRoleClaim<int>>
    {
        public ApplicationUserStore(IPOContext context) : base(context)
        {
        }
    }
}
