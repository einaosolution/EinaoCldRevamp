using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;
using IPORevamp.Data;
using IPORevamp.Data.Entities.Event;
using IPORevamp.Data.Entities.Modules;
using IPORevamp.Data.Entities.Setting;
using IPORevamp.Data.ViewModel;
using IPORevamp.Data.TempModel;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entities;
using IPORevamp.Data.Entity.Interface.Entities.Auth;
using IPORevamp.Data.Entity.Interface.Entities.Role;
using IPORevamp.Data.Entities.Menus;

namespace IPORevamp.Repository.Role
{
    public interface IRoleRepository : IAutoDependencyRegister
    {
        #region Role Respository
        Task<RoleManager> SaveRole(RoleManager roleManager);
        Task<RoleManager> GetRoleManagerById(int RoleId);
        Task<List<RoleManager>> GetAllRoles();
        Task<RoleManager> CheckExistingRole(string RoleName);   
        Task<string> UpdateRole(int id, List<int> roles, int RequestedBy);
        Task<RoleManager> EditRole(RoleManager roleManager);
        Task<RoleManager> DeleteRole(RoleManager deleteRole);
    
        Task<RoleManager> RolesExists(int id);

        #endregion

      


    }
}
