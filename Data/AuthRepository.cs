using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Matrix.Models;
using Microsoft.EntityFrameworkCore;

namespace Matrix.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string userName, string password)
        {
            var user = await _context.UserList.FirstOrDefaultAsync(u => u.UserName == userName);

            return (user == null || !PasswordHashVerified(password, user.PasswordHash, user.PasswordSalt)) ? null : user;
        }

        private bool PasswordHashVerified(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var hash = new HMACSHA512();
            hash.Key = passwordSalt;
            var enteredHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            if (enteredHash.SequenceEqual(passwordHash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<User> Register(string userName, string password)
        {
            // Hash the password using SHA512 with random key (salt)
            var hash = new HMACSHA512();
            var computedHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            var newUser = new User { UserName = userName };
            newUser.PasswordHash = computedHash;
            newUser.PasswordSalt = hash.Key;

            await _context.UserList.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return newUser;
        }
        public async Task<bool> UserNameValidity(string userName)
        {
            var UserFound = await _context.UserList.FirstOrDefaultAsync(u => u.UserName == userName);
            if (UserFound == null){
                return false;
            }else{
                return true;
            }
        }
    }
}
