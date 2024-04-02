using Jumia.Dtos.AccountDtos;
using Jumia.Model;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace JumiaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private UserManager<UserIdentity> _userManager;
        private SignInManager<UserIdentity> _signinManager;
        public AccountController(UserManager<UserIdentity> userManager, SignInManager<UserIdentity> signInManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
            _signinManager = signInManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDtos registerDtos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new UserIdentity() { UserName = registerDtos.Username, Email = registerDtos.Email, PhoneNumber = registerDtos.Phone };
                    IdentityResult res = await _userManager.CreateAsync(user, registerDtos.Password);

                    if (res.Succeeded && registerDtos.Password == registerDtos.Confirmpass)
                    {
                        await _signinManager.SignInAsync(user, isPersistent: false);
                        var role = await _userManager.AddToRoleAsync(user, "user");
                        return Created("User created successfully", user);
                    }
                    else
                    {
                        foreach (var error in res.Errors)
                        {
                            ModelState.AddModelError("Error", error.Description);
                        }
                        return BadRequest($"Invalid registration request.{res.Errors.ToString()}");
                    }
                }
                else
                {
                    return BadRequest("Invalid registration request.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDtos loginDtos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByNameAsync(loginDtos.Username);
                    if (user == null)
                    {
                        return Unauthorized("UserName Not Found");

                    }
                    else
                    {
                       
                        var CheckPass = await _userManager.CheckPasswordAsync(user, loginDtos.Password);
                        if (!CheckPass)
                        {
                            return Unauthorized("PassWord Is not Correct");
                        }
                       
                        var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                        var token = new JwtSecurityToken(
                            issuer: _configuration["Jwt:Issuer"],
                            audience: _configuration["Jwt:Audiences"], expires: DateTime.Now.AddDays(30),
                            signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials
                            (Key, SecurityAlgorithms.HmacSha256Signature));

                        //generate taken
                        var stringtaken = new JwtSecurityTokenHandler().WriteToken(token);
                        return Ok(new
                        {
                            stringtaken,
                            Expire = token.ValidTo,
                            userId= user.Id

                        });
                    }
                   
                   /* var result = await _signinManager.PasswordSignInAsync(loginDtos.Username, loginDtos.Password, false, false);

                    if (result.Succeeded)
                    {
                        return StatusCode(201, "success!");
                    }
                    else
                    {

                        return Unauthorized("invalid");
                    }*/
                }

                 return Unauthorized("invalid ");               
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }

}
