using Business.DTOs.Auth.Request;
using Business.DTOs.Auth.Response;
using Business.Services.Abstraction;
using Business.Validators.Auth;
using Core.Constants;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Response<AuthRegisterResponseDTO>> RegisterAsync(AuthRegisterDTO model)
        {
            var response = new Response<AuthRegisterResponseDTO>();
            var validator = new AuthRegisterDTOValidator();
            var result = await validator.ValidateAsync(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                    response.Errors.Add(error.ErrorMessage);

                response.Status = StatusCode.BadRequest;
                return response;
            }

            var user = new User
            {
                Email = model.Email,
                UserName = model.Username
            };

            var identityResult = await _userManager.CreateAsync(user, model.Password);
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                    response.Errors.Add(error.Description);

                response.Status = StatusCode.BadRequest;
                return response;
            }

            identityResult = await _roleManager.CreateAsync(new IdentityRole
            {
                Name = UserRoles.User.ToString()
            });

            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                    response.Errors.Add(error.Description);

                response.Status = StatusCode.BadRequest;
                return response;
            }

            identityResult = await _userManager.AddToRoleAsync(user, UserRoles.User.ToString());
            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                    response.Errors.Add(error.Description);

                response.Status = StatusCode.BadRequest;
                return response;
            }

            return response;
        }

        public async Task<Response<AuthLoginResponseDTO>> LoginAsync(AuthLoginDTO model)
        {
            var response = new Response<AuthLoginResponseDTO>();
            var validator = new AuthLoginDTOValidator();
            var result = await validator.ValidateAsync(model);
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                    response.Errors.Add(error.ErrorMessage);

                response.Status = StatusCode.BadRequest;
                return response;
            }

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                response.Errors.Add("İstifadəçi adı və ya şifrə yalnışdır");
                response.Status = StatusCode.Unauthorized;
                return response;
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }


            JwtSecurityToken securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(3)
            );

            response.Data = new AuthLoginResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken)
            };

            return response;
        }
    }
}
