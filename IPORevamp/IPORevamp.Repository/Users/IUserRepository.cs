﻿using IPORevamp.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using IPORevamp.Data.UserManagement.Model;

namespace IPORevamp.Repository.Users
{
    public interface ICountryRepository: IAutoDependencyRegister
    {
        ApplicationUser AuthenticateUser(string userName, string password);
        


    }
}
