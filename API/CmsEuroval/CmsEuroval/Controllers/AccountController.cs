using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EurovalBusinessLogic.Services.ViewModels;
using EurovalDataAccess;
using EurovalDataAccess.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly EurovalCmsContext _context;

        public AccountController(ILogger<AccountController> loggin,
            SignInManager<CmsUser> signInManager,
            UserManager<CmsUser> userManager,
            IConfiguration configuration,
            EurovalCmsContext context)
        {
            this._loggin = loggin;
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._configuration = configuration;
            this._context = context;
        }

        // PUT: api/Account/CreateToken
        /// <summary>
        /// Generates an authentification token for the user. It's associated with the AspNet Core Identity User.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        // GET: api/Account
        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<IEnumerable<CmsUser>> GetUsers()
        {
            try
            {
                return Ok(_userManager.Users.ToList());

            }
            catch (Exception ex)
            {

                _loggin.LogError($"Failed to get Users: {ex}");
                return BadRequest("Failed to get users");
            }
        }

        // GET: api/Account/5
        /// <summary>
        /// Get the user with the id (in Guid format) passed as parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CmsUser>> GetUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return NotFound($"User {id} not found");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _loggin.LogError($"Failed to get User: {ex}");
                return BadRequest("Failed to get User");
            }
        }

        // PUT: api/Account/5
        /// <summary>
        /// Updates a user. Be aware that only telephone number and ApiUser are updated. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IdentityResult>> PutUser([FromRoute] string id, [FromBody] CmsUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest("Id from route is different from entity");
            }

            try
            {

                var user2 = await _userManager.FindByIdAsync(user.Id);
                if (user2 == null)
                {

                }
                user2.PhoneNumber = user.PhoneNumber;
                user2.ApiUser = user.ApiUser;
                IdentityResult res = await _userManager.UpdateAsync(user2);
                if (res != IdentityResult.Success)
                {
                    return BadRequest($"User {id} Not found");
                }
            }
            catch (Exception ex)
            {
                _loggin.LogError($"Failed to Update User: {ex}");
                return BadRequest($"Failed to update user {user.Id}");
            }

            return NoContent();
        }

        // POST: api/Account
        /// <summary>
        /// Creates a new Admin user. The field Name, userName and PasswordHash are required.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<CmsUser>> PostUser([FromBody] CmsUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _userManager.CreateAsync(user, user.PasswordHash);
                if (result == IdentityResult.Success)
                {
                    result = await _userManager.AddToRoleAsync(user, "admin");
                    if (result != IdentityResult.Success)
                    {
                        return BadRequest(result);
                    }
                }
                else
                {
                    return BadRequest(result);
                }
                user = await _userManager.FindByEmailAsync(user.Email);
                return CreatedAtAction("GetUser", new { id = user.Id }, user);

            }
            catch (Exception ex)
            {
                _loggin.LogError($"Failed to create User: {ex}");
                return BadRequest("Failed Create User");
            }


        }

        /// <summary>
        /// Delete the user passed as parameter.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        // DELETE: api/Account/5
        [HttpDelete()]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IdentityResult>> DeleteUser([FromBody] CmsUser user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _userManager.DeleteAsync(user);
                if (result != IdentityResult.Success)
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                _loggin.LogError($"Failed to create User: {ex}");
                return BadRequest($"Failed to delete user");
            }

            return Ok(user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<bool> UserExists(string id)
        {
            return (await _userManager.FindByIdAsync(id)) != null;
        }


    }
}