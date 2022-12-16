using Business.DTOs.Auth.Request;
using Business.DTOs.Auth.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstraction
{
    public interface IAuthService
    {
        Task<Response<AuthRegisterResponseDTO>> RegisterAsync(AuthRegisterDTO model);
        Task<Response<AuthLoginResponseDTO>> LoginAsync(AuthLoginDTO model);
    }
}
