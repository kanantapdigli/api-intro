using Business.DTOs.Auth.Request;
using Business.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
           _authService = authService;
        }

        #region Documentation
        /// <summary>
        /// İstifadəçinin qeydiyyatdan keçməsi üçün istifadə olunur
        /// </summary>
        /// <returns></returns>
        #endregion
        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthRegisterDTO model)
        {
            return Ok(await _authService.RegisterAsync(model));
        }


        #region 
        /// <summary>
        /// İstifadəçinin daxil olması üçün istifadə olunur
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        #endregion
        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthLoginDTO model)
        {
            return Ok(await _authService.LoginAsync(model));
        }
    }
}
