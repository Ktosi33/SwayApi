﻿using Microsoft.AspNetCore.Identity;
using SwayApi.Exceptions;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace SwayApi.Services
{
    
    public class AccountService : IAccountService
    {
        private readonly SwayDbContext dbContext;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly AuthenticationSettings authenticationSettings;

        public AccountService(SwayDbContext dbContext, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
            this.authenticationSettings = authenticationSettings;
        }
        public string GetInformation(int id)
        {
            var user = dbContext.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == id);

            if (user is null)
            {
                throw new BadRequestException("Zły adres email lub hasło");
            }
            UserInformationDto dto = new UserInformationDto();
            dto.Id = id;
            dto.Email = WebUtility.HtmlEncode(user.Email);
            dto.RoleName = user.Role.Name;
            //TODO: Check it
            return JsonConvert.SerializeObject(dto);

        }
        public string GenerateJwt(LoginDto dto)
        {
           dto.Email = WebUtility.HtmlEncode(dto.Email);
            var user = dbContext.Users.FirstOrDefault(u => u.Email == dto.Email);
           
            if (user is null)
            {
                throw new BadRequestException("Zły adres email lub hasło");
            }
            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Zły adres email lub hasło");
            }
            return writeToken(dto.Email);


        }
        public string writeToken(string email)
        {
            var user = dbContext.Users.Include(u => u.Role).FirstOrDefault(u => u.Email == email);

            if (user is null)
            {
                throw new BadRequestException("Zły adres email");
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email.ToString()),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Double.Parse(authenticationSettings.JwtExpireDays));

            var token = new JwtSecurityToken(authenticationSettings.JwtIssuer,
                authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = WebUtility.HtmlEncode(dto.Email),
                RoleId = dto.RoleId,
            };
            var hashedPassword = passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPassword;

            dbContext.Users.Add(newUser); 
            dbContext.SaveChanges();
        }

    }
}
