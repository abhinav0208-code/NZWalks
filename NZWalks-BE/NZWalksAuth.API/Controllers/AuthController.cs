using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalksAuth.API.Models.DTO;
using NZWalksAuth.API.Repositories;

namespace NZWalksAuth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<IdentityUser> userManager;
        private ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> _userManager, ITokenRepository _tokenRepository)
        {
            this.userManager = _userManager;
            this.tokenRepository = _tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            IdentityUser identityUser = new IdentityUser()
            {

                Email = registerUserDto.UserName,
                UserName = registerUserDto.UserName
            };


            IdentityResult identityResult = await userManager.CreateAsync(identityUser, registerUserDto.Password);

            if (identityResult.Succeeded)
            {
                //Add roles for the user
                if (registerUserDto.Roles != null && registerUserDto.Roles.Length > 0)
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerUserDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User created successfully");
                    }
                }

            }

            return BadRequest("Something went wrong");

        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            IdentityUser? identityUser = await userManager.FindByEmailAsync(userLoginDto.UserName);

            if (identityUser != null)
            {
                bool isPasswordCorrect = await userManager.CheckPasswordAsync(identityUser, userLoginDto.Password);
                if (isPasswordCorrect)
                {
                    //create token

                    IList<string> roles = await userManager.GetRolesAsync(identityUser);
                    string jwtToken = tokenRepository.GetJWTToken(identityUser, roles.ToList());
                    return Ok(new LoginResponseDto()
                    {
                        JwtToken = jwtToken
                    });
                }
            }

            return BadRequest("UserName or Password incorrect");

        }
    }
}
