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

namespace IPORevamp.Repository.Menu
{
    public interface IMenuRepository : IAutoDependencyRegister
    {
        #region Menu Respository
        Task<MenuManager> SaveMenu(MenuManager Menu);
        Task<MenuManager> GetMenuByName(string MenuName);
        Task<List<MenuManager>> GetMenus();
        Task<MenuManager> CheckExistingMenu(string MenuName);
        Task<MenuManager> GetMenuById(int MenuId);
        Task<MenuManager> UpdateMenu(MenuManager Menu);
        Task<MenuManager> DeleteMenu(MenuManager Menu);

        #endregion

        #region MyRegion

        List<LinkRolesMenus> GetLinkRolesMenus();
        Task<List<LinkRolesMenus>> GetLinkRolesMenusById(int Id);
        List<LinkRolesMenus> GetMenuManagers();
        #endregion



    }
}
