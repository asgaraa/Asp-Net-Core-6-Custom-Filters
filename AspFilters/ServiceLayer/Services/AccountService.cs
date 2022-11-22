using AutoMapper;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.DTOs.AppUser;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class AccountService:IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService;
        //private readonly IUserRepository _repository;
        public AccountService(UserManager<AppUser> userManager,
                                 IMapper mapper,
                                 ITokenService tokenService,
                                 IEmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
            _emailService = emailService;
            //_repository = repository;
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null) return null;

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password)) return null;

            var roles = await _userManager.GetRolesAsync(user);

            string token = _tokenService.GenerateJwtToken(user.Id, user.Email, user.UserName, (List<string>)roles);

            return token;
        }

        public async Task Register(RegisterDto registerDto)
        {
            var user = _mapper.Map<AppUser>(registerDto);
            await _userManager.CreateAsync(user, registerDto.Password);
            await _userManager.AddToRoleAsync(user, "User");

        }
        public async Task Update(AppUser appUser, UpdateUserDto updateUserDto)
        {


            var appuse = await _userManager.FindByIdAsync(appUser.Id);
            if (updateUserDto.FullName != null)
            {
                appuse.FullName = updateUserDto.FullName;
            }
            if (updateUserDto.PhoneNumber != null)
            {
                appuse.PhoneNumber = updateUserDto.PhoneNumber;
            }
            if (updateUserDto.UserName != null)
            {
                appuse.UserName = updateUserDto.UserName;
            }
            var upuser = await _userManager.UpdateAsync(appuse);




        }

        public async Task UpdatePassword(AppUser appUser, UpdatePasswordDto updatePasswordDto)
        {
            var appuse = await _userManager.FindByIdAsync(appUser.Id);


            //var result = await _userManager.RemovePasswordAsync(appuse);
            //result = await _userManager.AddPasswordAsync(appuse, updatePasswordDto.Password);

            var result = await _userManager.ChangePasswordAsync(appuse, updatePasswordDto.CurrentPassword, updatePasswordDto.NewPassword);
            var user = _mapper.Map(updatePasswordDto, appuse);
            var upuser = await _userManager.UpdateAsync(user);


        }

        public async Task ConfirmEmail(string userId, string token)
        {
            await _emailService.ConfirmEmail(userId, token);

        }
        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var appuser = await _userManager.FindByEmailAsync(email);
            var user = _mapper.Map<UserDto>(appuser);
            return user;
        }
    }
}
