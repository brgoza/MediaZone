using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaZone.Data.Entities.Identity;
using MediaZone.Services.Interfaces;
using MediaZone.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MediaZone.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;

    private readonly IConfiguration _config;
    private readonly ILogger<IdentityService> _logger;
    private readonly string _adminRoleName;
    public IdentityService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration config, ILogger<IdentityService> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
        _logger = logger;

        string? adminRoleName = _config.GetSection("RoleScheme:AdminRoleName").Value;
        if (adminRoleName == null)
        {
            _logger.LogError("Config value \"RoleScheme:AdminRoleName\" is null.  Throwing exception.");
            throw new Exception("Config value \"RoleScheme:AdminRoleName\" is null.");
        }

        _adminRoleName = adminRoleName!;
    }
    public async Task<AppUser?> GetUser(string userName) => await _userManager.FindByNameAsync(userName);

    public async Task<AppUser?> GetUser(Guid userId) => await _userManager.FindByIdAsync(userId.ToString());


    public async Task<AppRole> GetAdminRole()
    {
        AppRole? adminRole = await _roleManager.FindByNameAsync(_adminRoleName);
        if (adminRole is not null) return adminRole;
        
        //ELSE MAKE ADMIN ROLE, THROWING EXCEPTION ON ERROR
        IdentityResult roleResult = await _roleManager.CreateAsync(new AppRole() { Name = _adminRoleName});
        if (roleResult.Succeeded)
        {
            _logger.LogInformation("Admin role created");
            return await _roleManager!.FindByNameAsync(_adminRoleName) ?? throw new("error retrieving new admin role");
        }
        else
        {
            string roleErrorMsg = string.Format("Error(s) creating admin role: {0}", string.Join('\n', roleResult.Errors.Select(e => e.Description)));
            _logger.LogError("{errMsg}", roleErrorMsg);
            throw new Exception(roleErrorMsg);
        }
    }


    public async Task<IdentityResult> CreateUser(AppUser appUser, string password, IEnumerable<AppRole>? roles = null)
    {
        if (roles is null) return await CreateUser(appUser, password, isAdmin: false);
        appUser.Roles = roles;
        return await _userManager.CreateAsync(appUser, password);

    }
    public async Task<IdentityResult> CreateUser(AppUser appUser, string password, bool isAdmin = false)
    {
        if (isAdmin) { appUser.Roles.ToList().Add(await GetAdminRole()); }
        IdentityResult userResult = await _userManager.CreateAsync(appUser,password);
        if (!userResult.Succeeded)
        {
            string userErrorMsg = string.Format("Error(s) creating user: {0}", string.Join('\n', userResult.Errors.Select(e => e.Description)));
            _logger.LogWarning("{errMsg}", userErrorMsg);
        }
        else
        {
            _logger.LogInformation("user {userName} created.\nroles:\t {roles}", appUser.UserName, string.Join(',',appUser.Roles.Select(r=>r.Name)));
        }

        return userResult;
    }


}



