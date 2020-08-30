using CRUD.Helpers;
using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.Auth
{
    public interface IAuthRepository
    {
        Task<ResponseData<User>> Register(User user, string password);
        Task<ResponseData<string>> Login(string username, string password);
        Task<bool> UserExists(string username);
        Task<ResponseData<string>> ChangePassword(string username, string oldPassword, string newPassword);

        Task SaveChanges();
    }
}
