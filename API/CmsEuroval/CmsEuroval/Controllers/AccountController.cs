using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EurovalBusinessLogic.Services.ViewModels;
using EurovalDataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CmsEuroval.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _loggin;
        private readonly SignInManager<CmsUser> _signInManager;
        private readonly UserManager<CmsUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(ILogger<AccountController> loggin,
            SignInManager<CmsUser> signInManager,
            UserManager<CmsUser> userManager,
            IConfiguration configuration)
        {
            this._loggin = loggin;
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._configuration = configuration;
        }

        // PUT: api/Account/CreateToken
        [Route("CreateToken")]
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(user);
                        //Create Tokens
                        var Claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub.ToString(), user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),//Jti: Unique string for each claims
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),//UniqueName: Unique is the name of the user that maps in the identity object avaliable in every controller
                            new Claim(ClaimTypes.Role, roles.First())
                        };

                        

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            _configuration["Tokens:Issuer"],
                            _configuration["Tokens:Audience"],
                            Claims,
                            expires: DateTime.UtcNow.AddMinutes(30),
                            signingCredentials: creds
                            );

                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return Created("", results);
                    }
                }
            }

            return BadRequest(ModelState);
        }

    }
}