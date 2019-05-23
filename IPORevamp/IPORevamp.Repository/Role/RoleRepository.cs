using EmailEngine.Base.Entities;
using EmailEngine.Repository.EmailRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IPORevamp.Repository.Interface;
using IPORevamp.Core.Utilities;
using IPORevamp.Data;
using IPORevamp.Data.Entities.AuditTrail;


using IPORevamp.Data.Entities.Payment;
using IPORevamp.Data.UserManagement.Model;

using IPORevamp.Data.Entities.Setting;
using IPORevamp.Data.ViewModel;
using System.Data.SqlClient;
using IPORevamp.Data.TempModel;
using IPORevamp.Data.Entities.Email;
using IPORevamp.Data.Entities.Country;
using IPORevamp.Data.Entities.LGAs;
using IPORevamp.Data.Entities;
using IPORevamp.Data.Entity.Interface.Entities.Role;
using IPORevamp.Data.Entity.Interface.Entities.Auth;
using IPORevamp.Data.Entities.Menus;

namespace IPORevamp.Repository.Role
{
    public class RoleRepository : IRoleRepository
    {
        private IRepository<RoleManager> _rolerepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;
        private IRepository<LinkRolesMenus> _linkRolesMenusrepository;
        private IRepository<MenuManager> _menuManagerrepository;

        public RoleRepository(IRepository<RoleManager> Rolerepository,
                                IRepository<LinkRolesMenus> LinkRolesMenusrepository,
                                 IRepository<MenuManager> menuManagerrepository,
                                IAuditTrailManager<AuditTrail> auditTrailManager)
        {
            _rolerepository = Rolerepository;
            _auditTrailManager = auditTrailManager;
            _linkRolesMenusrepository = LinkRolesMenusrepository;
            _menuManagerrepository = menuManagerrepository;


        }



        // Get Role By Id
        public async Task<RoleManager> GetRoleById(int RoleId)
        {

            RoleManager entity = new RoleManager();

            entity = await _rolerepository.GetAll().FirstOrDefaultAsync(x => x.Id == RoleId);


            return entity;
        }


        // Get All Roles 
        public async Task<List<RoleManager>> GetAllRoles()
        {


            List<RoleManager> entity = new List<RoleManager>();
            entity = await _rolerepository.GetAllListAsync();
            return entity;

        }


        public async Task<RoleManager> GetRoleManagerById(int RoleId)
        {
            RoleManager country = new RoleManager();
            country = await _rolerepository.GetAll().FirstOrDefaultAsync(x => x.Id == RoleId);

            return country;
        }


        public async Task<RoleManager> RolesExists(int RoleId)
        {
            RoleManager country = new RoleManager();
            country = await _rolerepository.GetAll().FirstOrDefaultAsync(x => x.Id == RoleId);

            return country;
        }
        public async Task<string> UpdateRole(int id, List<int> AssignedRoles, int CurrentRole)

        {
            try
            {
                var temp = _linkRolesMenusrepository.GetAll().Where(a => a.RolesId == id);

                foreach (var item in temp)
                {
                    _linkRolesMenusrepository.Remove(item);
                   // _linkRolesMenusrepository.SaveChangesAsync();
                }
                List<LinkRolesMenus> linkRolesMenus = new List<LinkRolesMenus>();
                LinkRolesMenus linkRolesMenus_ = new LinkRolesMenus();


                foreach (var role in AssignedRoles)
                {
                    var  d= (new LinkRolesMenus { MenusId = role, RolesId = id, CreatedBy = CurrentRole.ToString(),
                                                            IsActive = true,DateCreated=DateTime.Now,IsDeleted=false });
                    _linkRolesMenusrepository.Insert(d);
                        
                        }

               
               await _linkRolesMenusrepository.SaveChangesAsync();

                return WebApiMessage.UpdateRequest;
            }
            catch (Exception ex)
            {

                return WebApiMessage.FailUpdateRequest;
            }

        }


        public async Task<RoleManager> DeleteRole(RoleManager entity)
        {
            var saveContent = await _rolerepository.UpdateAsync(entity);
            await _rolerepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        public async Task<RoleManager> SaveRole(RoleManager entity)
        {
            

            var saveContent = await _rolerepository.InsertAsync(entity);
            await _rolerepository.SaveChangesAsync();

            return saveContent.Entity;
        }
       
        // Get Country By Name

        public async Task<RoleManager> GetRoleByName(string RoleName)
        {

          var content  = await _rolerepository.GetAll().FirstOrDefaultAsync(x => x.Title.ToUpper() == RoleName.ToUpper());
            return content;
        }

        public async Task<RoleManager> CheckExistingRole(string RoleName)
        {
            RoleManager content = new RoleManager();

            content = await _rolerepository.FirstOrDefaultAsync(x => x.Title.ToUpper() == RoleName.ToUpper());


            return content;
        }

        public async  Task<RoleManager> EditRole(RoleManager roleManager)
        {
            var entity = await _rolerepository.UpdateAsync(roleManager);
            await _rolerepository.SaveChangesAsync();

            return entity.Entity;
        }

       
    }
}
