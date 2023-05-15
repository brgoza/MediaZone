using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaZone.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace MediaZone.Services.Interfaces;

public interface IIdentityService
{
    public Task<AppUser?> GetUser(string userName);
    public Task<AppUser?> GetUser(Guid userId);

    public Task<AppRole> GetAdminRole();
    public Task<IdentityResult> CreateUser(AppUser appUser, string password, bool isAdmin = false);
    public Task<IdentityResult> CreateUser(AppUser appUser, string password, IEnumerable<AppRole>? roles = null);

}
