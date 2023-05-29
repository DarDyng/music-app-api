﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MusicAppApi.Core.interfaces.Services;
using MusicAppApi.Models.DbModels;

namespace MusicAppApi.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CheckRoleExists(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<bool> CreateRole(Role role)
        {
            var res = await _roleManager.CreateAsync(role);
            
            return res.Succeeded;
        }
    }
}
