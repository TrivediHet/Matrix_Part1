using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matrix.Models;

namespace Matrix.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(string user, string password);
        Task<User> Login(string userName, string password);
        Task<bool> UserNameValidity(string UserName);
    }
}
