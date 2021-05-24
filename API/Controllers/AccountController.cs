using API.Dtos;
using API.Exceptions;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO login) {
            var user = await userManager.FindByEmailAsync(login.Email);

            if (user == null)
                return Unauthorized(new ApiResponse(401));

            var result = await signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if(!result.Succeeded)
                return Unauthorized(new ApiResponse(401));

            return Ok(new UserDTO { 
                Email = user.Email, 
                DisplayName = user.DisplayName, 
                Token ="this will be token"
             });
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDTO register) {

            var user = new AppUser { 
                DisplayName = register.DisplayName, 
                Email = register.Email, 
                UserName = register.Email
            };

            var result = await userManager.CreateAsync(user, register.Password);

            if(!result.Succeeded)
                return BadRequest(new ApiResponse(400));

            return Ok(new UserDTO { 
                Email = user.Email,
                DisplayName = user.DisplayName, 
                Token = "this will be token" 
            });
        }
    }
}
