using BookStore.Api.Errors;
using BookStore.Core.Dtos;
using BookStore.Core.Entities;
using BookStore.Core.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
   
    public class AccountController : ApiBaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthService _authService;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,IAuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
        }
        [ProducesResponseType(typeof(UserDto),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status200OK)]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var userCheck = await _userManager.FindByEmailAsync(registerDto.Email);
            if(userCheck != null) { return BadRequest(new ApisResponse(404, "email in use try another one"));}
           
            var user = new ApplicationUser()
            { 
                DisplayName=registerDto.DisplayName,
                Email=registerDto.Email,
                PhoneNumber=registerDto.PhoneNumber,
                UserName = registerDto.Email.Split('@')[0]
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest();

            return Ok(new UserDto()
            { 
                DisplayName= user.UserName,
                Email=user.Email,
                Token = "Please LogIn To Get Token"
            });


        }
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApisResponse), StatusCodes.Status401Unauthorized)]
        [HttpPost("logIn")]
        public async Task<ActionResult<UserDto>> LogIn(LogInDto logInDto)
        {
            var user =await _userManager.FindByEmailAsync(logInDto.Email);
            if (user == null) return Unauthorized(new ApisResponse(401, "Invalid LogIn"));
            var result =await _signInManager.CheckPasswordSignInAsync(user, logInDto.Password,false);
            if (!result.Succeeded) return BadRequest(new ApisResponse(400, "Invalid LogIn"));
            return Ok(new UserDto()
            {
                DisplayName = user.UserName,
                Email = user.Email,
                Token =await  _authService.CreateTokeAync(user,_userManager)
            });
        }
    }
}
