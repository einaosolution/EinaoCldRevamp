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
using IPORevamp.Data.Entities;
using IPORevamp.Data.Entities.LGAs;
using IPORevamp.Repository.Menu;
using IPORevamp.Data.Entities.Menus;
using IPORevamp.Data.Entity.Interface.Entities.Role;

namespace IPORevamp.Repository.Menu
{ 
    public class MenuRepository : IMenuRepository
    {
        private IRepository<Data.Entities.Menus.MenuManager> _menurepository;
        private IRepository<LinkRolesMenus> _linkRolesMenusrepository;
        private IAuditTrailManager<AuditTrail> _auditTrailManager;

        public MenuRepository(IRepository<Data.Entities.Menus.MenuManager> menurepository, IAuditTrailManager<AuditTrail> auditTrailManager, IRepository<LinkRolesMenus> LinkRolesMenusrepository
           )
        {
            _menurepository = menurepository;
            _auditTrailManager = auditTrailManager;
            _linkRolesMenusrepository = LinkRolesMenusrepository;

        }



        // Get Menu By Id
        public async Task<Data.Entities.Menus.MenuManager> GetMenuById(int MenuId, bool IncludeStates, bool IncludeStateLGAs)
        {

            var content = await _menurepository.GetAll().FirstOrDefaultAsync(x => x.Id == MenuId);

            return content;
        }


        // Get All Countries 
        public async Task<List<Data.Entities.Menus.MenuManager>> GetMenus()
        {
            List<Data.Entities.Menus.MenuManager> Menu = new List<Data.Entities.Menus.MenuManager>();
            Menu = await _menurepository.GetAllListAsync();
            return Menu;
        }


        public async Task<List<MenuManager>> GetAllParentMenu()
        {
            List<Data.Entities.Menus.MenuManager> Menu = new List<Data.Entities.Menus.MenuManager>();
            Menu = await _menurepository.GetAll().Where(a => a.ParentId == 0).ToListAsync();
            return Menu;
        }

        public async Task<List<Data.Entities.Menus.MenuManager>> GetAllParentChildMenu(int ParentId)
        {
            List<Data.Entities.Menus.MenuManager> Menu = new List<Data.Entities.Menus.MenuManager>();
           // Menu = await _menurepository.GetAll().Where(a =>  a.Id== ParentId).ToListAsync();
            Menu = await _menurepository.GetAll().Where(a => a.ParentId != ParentId).ToListAsync();
            return Menu;
        }


        public async Task<Data.Entities.Menus.MenuManager> UpdateMenu(Data.Entities.Menus.MenuManager Menu)
        {
            var saveContent = await _menurepository.UpdateAsync(Menu);
            await _menurepository.SaveChangesAsync();

            return saveContent.Entity;
        }


        public async Task<Data.Entities.Menus.MenuManager> DeleteMenu(Data.Entities.Menus.MenuManager Menu)
        {
            var saveContent = await _menurepository.UpdateAsync(Menu);
            await _menurepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        public async Task<Data.Entities.Menus.MenuManager> SaveMenu(Data.Entities.Menus.MenuManager Menu)
        {

            var saveContent = await _menurepository.InsertAsync(Menu);
            await _menurepository.SaveChangesAsync();

            return saveContent.Entity;
        }

        // Get Menu By Name

        public async Task<Data.Entities.Menus.MenuManager> GetMenuByName(string Description)
        {

            var Menu = await _menurepository.GetAll().FirstOrDefaultAsync(x => x.Name.ToUpper() == Description.ToUpper());
            return Menu;
        }

        public async Task<Data.Entities.Menus.MenuManager> CheckExistingMenu(string Description)
        {
            Data.Entities.Menus.MenuManager Menu = new Data.Entities.Menus.MenuManager();

            Menu = await _menurepository.GetAll().FirstOrDefaultAsync(x => x.Name.ToUpper() == Description.ToUpper());


            return Menu;
        }

        public async Task<Data.Entities.Menus.MenuManager> GetMenuById(int MenuId)
        {
            Data.Entities.Menus.MenuManager Menu = new Data.Entities.Menus.MenuManager();
            Menu = await _menurepository.GetAll().FirstOrDefaultAsync(x => x.Id == MenuId);

            return Menu;
        }


        public async Task<List<LinkRolesMenus>> GetLinkRolesMenus()
        {
            return await _linkRolesMenusrepository.GetAllListAsync();
        }

        public async Task<List<LinkRolesMenus>> GetLinkRolesMenusById(int Id)
        {
            var content = await _linkRolesMenusrepository.GetAll().Where(a => a.Id == Id).ToListAsync();
            return content;
        }

        public List<MenuManager> GetMenuManagers()
        {
            return _menurepository.GetAllList();
        }

        List<LinkRolesMenus> IMenuRepository.GetLinkRolesMenus()
        {
            return _linkRolesMenusrepository.GetAll().Include(a => a.Menus).ToList();
        }

        List<LinkRolesMenus> IMenuRepository.GetMenuManagers()
        {
            return _linkRolesMenusrepository.GetAll().Include(a => a.Menus).ToList();
        }
    }
}
