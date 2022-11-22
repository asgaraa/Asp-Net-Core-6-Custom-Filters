using DomainLayer.Entities;
using ServiceLayer.DTOs.AppUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
    public interface IAccountService
    {

        Task Register(RegisterDto registerDto);
        Task Update(AppUser appUser, UpdateUserDto updateuserDto);
        Task UpdatePassword(AppUser appUser, UpdatePasswordDto updatePasswordDto);
        Task<string> Login(LoginDto loginDto);
        Task ConfirmEmail(string userId, string token);
        Task<UserDto> GetUserByEmailAsync(string email);
    }
}
