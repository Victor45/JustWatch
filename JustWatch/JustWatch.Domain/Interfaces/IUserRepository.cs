using JustWatch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustWatch.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync (string email);
        Task AddAsync(User user);
        Task<User?> GetByEmailAsync (string email);
    }
}
