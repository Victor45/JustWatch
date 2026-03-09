using JustWatch.Application.DTO;
using JustWatch.Domain.Commom;
using JustWatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Application.Interfaces
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(RegisterRequestDTO registerRequest);
        Task<Result<User>> LoginAsync(LoginRequestDTO loginRequest);
    }
}
