using FundooNotesManagerLayer.IManager;
using FundooNotesModelLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserManager userManager;
        private readonly IConfiguration configuration;
        private readonly IDistributedCache cache;
        private readonly string cacheKey;

        public UserController(IUserManager userManager, IConfiguration configuration, IDistributedCache cache)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.cache = cache;
            this.cacheKey = "User";
        }
        [HttpPost]
        public ActionResult AddUser(UserRegistration user)
        {
            try
            {
                var result = this.userManager.AddUser(user);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "User Added Successfully", Data = result });
                }
                return this.BadRequest(new { Status = false, Message = "User Added UnSuccessfully" });

            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });

            }
        }
       
        [HttpPut]
        public ActionResult ResetPassword(ResetUserPassword user)
        {
            try
            {
                if (user.Password != user.ConfirmPassword)
                {
                    return this.BadRequest(new { Status = false, Message = "password match unsuccessfull" });
                }
                var result = this.userManager.ResetPassword(user);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Password reset Successfully", Data = result });
                }
                return this.NotFound(new { Status = false, Message = "Password reset UnSuccessfully" });

            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });

            }
        }
        [HttpGet]
        [Route("ForgotPassword/{email}")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                ForgetPassword forget = new ForgetPassword();
                forget.Email = email;
                var result = this.userManager.ForgetPassword(forget);

                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Password Send Successfully", Data = result });
                }
                return this.NotFound(new { Status = false, Message = "Sending Password Failed" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });
            }
        }
        [HttpGet]
        public ActionResult GetAllUser()
        {
            try
            {
                var result = this.userManager.GetAllUser();
                if (result != null)
                {
                    this.cache.SetString(this.cacheKey, JsonConvert.SerializeObject(result));
                    
                }
                if(this.cache.GetString(cacheKey)!=null)
                {
                    var data = JsonConvert.DeserializeObject<List<UserRegistration>>(this.cache.GetString(cacheKey));
                    return this.Ok(new { Status = true, Message = "User Get Successfully", Data = data });
                }
                return this.BadRequest(new { Status = false, Message = "User Get UnSuccessfully" });

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });

            }
        }
        [HttpPost]
        [Route("login")]
        public ActionResult Login(UserLogin login)
        {
            try
            {
                var result = this.userManager.Login(login);
                if (result!=null)
                {
                   
                    var token = GenrateJWTToken(result.Email,result.UserId);
                    return this.Ok(new { Status = true, Message = "User Varified Successfully", Data = token});
                }
                return this.NotFound(new { Status = false, Message = "User Verified UnSuccessfully" });

            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = false, Message = e.Message });

            }
        }
        private string GenrateJWTToken(string Email, long userId)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Key"]));
            var signinCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim("Email",Email),
                new Claim("userId",userId.ToString())
            };
            var tokenOptionOne = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: signinCredentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptionOne);
            return token;
        }

    }
}
