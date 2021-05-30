using API.Dtos;
using API.Exceptions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly ITokenService tokenService;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, 
                                 ITokenService tokenService, IMapper mapper) {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetCurrentUser() {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.FindByEmailAsync(email);

            var result = mapper.Map<UserDTO>(user);

            result.Token = tokenService.CreateToken(user);

            return Ok(result);
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult> CheckEmailExistsAsync([FromQuery]string email) {
            var result = await userManager.FindByEmailAsync(email) != null;

            return Ok(result);
        }

        [HttpGet("address")]
        [Authorize]
        public async Task<ActionResult> GetUserAdress() {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.Users
                .Include(u => u.Address)
                .SingleOrDefaultAsync(x => x.Email == email);

            return Ok(mapper.Map<AddressDTO>(user.Address));
        }

        [HttpPut("address")]
        [Authorize]
        public async Task<ActionResult> UpdateAddress([FromBody] AddressDTO address) {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var user = await userManager.Users
                .Include(u => u.Address)
                .FirstOrDefaultAsync(x => x.Email == email); //needed to use this dude to Exception when using SingleOrDefault raised by .NET

            user.Address = mapper.Map<Address>(address);

            var result = await userManager.UpdateAsync(user);

            if(!result.Succeeded)
                return BadRequest(new ApiResponse(400));

            return Ok(mapper.Map<AddressDTO>(user.Address));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDTO login) {
            var user = await userManager.FindByEmailAsync(login.Email);

            if (user == null)
                return Unauthorized(new ApiResponse(401));

            var passwordCheck = await signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if(!passwordCheck.Succeeded)
                return Unauthorized(new ApiResponse(401));

            var result = mapper.Map<UserDTO>(user);

            result.Token = tokenService.CreateToken(user);

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDTO register) {

            var emailExists = await userManager.FindByEmailAsync(register.Email);

            if (emailExists != null) {
                return new BadRequestObjectResult(
                    new ApiValidationErrorResponse { Errors = new[] { "Email address is in use" } });
            }

            var user = mapper.Map<AppUser>(register);

            var created = await userManager.CreateAsync(user, register.Password);

            if(!created.Succeeded)
                return BadRequest(new ApiResponse(400));

            var result = mapper.Map<UserDTO>(user);

            result.Token = tokenService.CreateToken(user);

            return Ok(result);
        }
    }
}
