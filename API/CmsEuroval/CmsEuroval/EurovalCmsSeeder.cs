using EurovalDataAccess;
using EurovalDataAccess.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsEuroval
{
    public class EurovalCmsSeeder
    {
        private readonly EurovalCmsContext _ctx;
        private readonly UserManager<CmsUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EurovalCmsSeeder(EurovalCmsContext ctx, UserManager<CmsUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _ctx = ctx;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            _ctx.Database.EnsureCreated();

            IdentityRole role = await _roleManager.FindByNameAsync("admin");
            if(role == null)
            {
                var roleResult = _roleManager.CreateAsync(new IdentityRole { Id = "admin", Name = "admin", NormalizedName = "admin", ConcurrencyStamp = DateTime.Now.Ticks.ToString() });
                if(roleResult.Result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user role");
                }
            }

            CmsUser user = await _userManager.FindByEmailAsync("jose@euroval.com");
            if (user == null)
            {
                user = new CmsUser
                {
                    ApiUser = true,
                    Email = "jose@euroval.com",
                    UserName = "jose@euroval.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result == IdentityResult.Success)
                {
                    result = await _userManager.AddToRoleAsync(user, "admin");
                    if(result!= IdentityResult.Success)
                    {
                        throw new InvalidOperationException("Could not add  user to role");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }
        }
    }
}
