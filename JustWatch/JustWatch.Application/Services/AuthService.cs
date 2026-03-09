using JustWatch.Application.DTO;
using JustWatch.Application.Interfaces;
using JustWatch.Domain.Commom;
using JustWatch.Domain.Interfaces;
using JustWatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        public AuthService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result> RegisterAsync(RegisterRequestDTO registerRequest)
        {
            if(await _userRepository.EmailExistsAsync(registerRequest.Email))
            {
                return Result.Error("This email is already in use");
            }

            var user = new User
            {
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Email = registerRequest.Email,
                Password = _passwordHasher.Hash(registerRequest.Password)
            };

            await _userRepository.AddAsync(user);
            return Result.Success();
        }

        public async Task<Result<User>> LoginAsync(LoginRequestDTO loginRequest)
        {
            var user = await _userRepository.GetByEmailAsync(loginRequest.Email);

            if (user == null || !_passwordHasher.Verify(loginRequest.Password, user.Password))
            {
                return Result<User>.Error("Invalid credentials");
            }

            return Result<User>.Success(user);
        }
    }
}
