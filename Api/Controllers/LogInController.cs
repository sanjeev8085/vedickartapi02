
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VadicKart.Entity.Presentation.Dto.ContactMessage;
using VadicKart.Entity.Presentation.Dto.LogIn;
using VedicKart.Service.Contract;
using vedickartApi.Api.ProfileMapping;

namespace vedickartApi.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class LoginController(IServiceManager service) : ControllerBase
    {
        private readonly IServiceManager _service = service;

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] UserRegistrationDto userRegistrationDto)
        {
            try
            {
                // Validate input
                if (userRegistrationDto == null)
                {
                    return BadRequest(new { message = "Login data is required" });
                }

                if (string.IsNullOrEmpty(userRegistrationDto.EMail) ||
                    string.IsNullOrEmpty(userRegistrationDto.Password) ||
                    userRegistrationDto.EMail == "string" ||
                    userRegistrationDto.Password == "string")
                {
                    return BadRequest(new { message = "Enter a valid email and password" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid input data", errors = ModelState });
                }

                // Attempt login
                var loginResult = await _service.LogInService.LogIn(userRegistrationDto);

                if (loginResult == "false")
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }

                // Return successful response with JWT token
                return Ok(new
                {
                    message = "Login successful",
                    token = loginResult,
                    tokenType = "Bearer",
                    email = userRegistrationDto.EMail
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during login", error = ex.Message });
            }
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] Sign_up_Dto signUpDto)
        {
            try
            {
                if (signUpDto == null)
                {
                    return BadRequest(new { message = "Signup data is required" });
                }

                // Validate input
                if (string.IsNullOrEmpty(signUpDto.EMail) ||
                    string.IsNullOrEmpty(signUpDto.Password) ||
                    string.IsNullOrEmpty(signUpDto.First_name) ||
                    string.IsNullOrEmpty(signUpDto.Last_name) ||
                    string.IsNullOrEmpty(signUpDto.UserName) ||
                    signUpDto.EMail == "string" ||
                    signUpDto.Password == "string" ||
                    signUpDto.First_name == "string" ||
                    signUpDto.Last_name == "string" ||
                    signUpDto.UserName == "string")
                {
                    return BadRequest(new { message = "All fields are required and cannot be placeholder values" });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new { message = "Invalid input data", errors = ModelState });
                }

                var signupResult = await _service.Sign_up_service.signup(signUpDto);
                return Ok(new { message = "Signup successful", data = signupResult });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during signup", error = ex.Message });
            }
        }

       
    }
}