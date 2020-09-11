using CRUD.Helpers;
using CRUD.Models;
using CRUD.Models.FactoryDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CRUD.Auth
{
    public class AuthRepository
    {
        private readonly IConfiguration _config;
        private readonly FactoryDbContext _context;

        public AuthRepository(FactoryDbContext context, IConfiguration config)
        {
            _config = config;
            _context = context;
        }

        public async Task<ResponseData<string>> Login(string username, string password)
        {
            var returnedRes = new ResponseData<string>();
            var user = await _context.Users.Include("Role").FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
            if (user == null || !VerifyPasswordHash(password, user.PasswordHash))
            {
                returnedRes.Success = false;
                returnedRes.Message = "Incorrect username or password";
            }
            else returnedRes.Data = CreateToken(user);
            return returnedRes;
        }

        public async Task<ResponseData<User>> Register(User user, string password)
        {
            var hash = GetPasswordHash(password);
            user.PasswordHash = hash;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return new ResponseData<User> { Data = user, Message = "Successfully created a user" };
        }


        public async Task<bool> UserExists(string username)
        {
            var userExists = await _context.Users.AnyAsync(user =>
                    username.ToLower() == user.Username.ToLower());
            return userExists;
        }

        private byte[] GetPasswordHash(string password)
        {
            using (var hasher = new System.Security.Cryptography.SHA512Managed())
            {
                return hasher.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash)
        {
            using (var hasher = new System.Security.Cryptography.SHA512Managed())
            {
                var hash = hasher.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < hash.Length; i++)
                {
                    if (hash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role?.Name)
            };

            // Secret from appsettings.json
            var secret = _config.GetSection("AppSettings:JwtSecret").Value;

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));

            // Signing credentials for signing the JWT token
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Creating the complete token with claims, expiration date and signing creds
            // Step 1 - create descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = signingCredentials
            };
            // Step 2 - add TokenHandler
            var tokenHandler = new JwtSecurityTokenHandler();
            // Step 3 - create the token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // WriteToken serializes it into a string
            return tokenHandler.WriteToken(token);
        }

        public async Task<ResponseData<string>> ChangePassword(string username, string oldPassword, string newPassword)
        {

            var user = await _context.Users.FirstOrDefaultAsync(user => user.Username.ToLower() == username.ToLower());

            if (user == null) return new ResponseData<string> { Success = false, Message = "Неверное имя пользователя" };
            if (!VerifyPasswordHash(oldPassword, user.PasswordHash))
                return new ResponseData<string> { Success = false, Message = "Неверный пароль" };

            user.PasswordHash = GetPasswordHash(newPassword);
            await _context.SaveChangesAsync();
            return new ResponseData<string> { Message = "Пароль успешно изменен" };

        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
