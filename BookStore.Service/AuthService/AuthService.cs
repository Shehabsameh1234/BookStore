﻿using BookStore.Core.Entities;
using BookStore.Core.Service.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokeAync(ApplicationUser user, UserManager<ApplicationUser> userManager)
        {
            #region PrivareClaims

            var authClaims = new List<Claim>()
            { 
                new Claim(ClaimTypes.Name,user.DisplayName),
                new Claim(ClaimTypes.Email , user.Email),
            };
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            #endregion
            #region AuthKey
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:AuthKey"]));
            #endregion
            #region GenerateToken
            var token = new JwtSecurityToken(
              audience: _configuration["jwt:validAudience"],
              issuer: _configuration["jwt:validIssuer"],
              expires: DateTime.Now.AddDays(double.Parse(_configuration["jwt:DurationInDays"])),
              claims:authClaims,
              signingCredentials:new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
            #endregion
        }
    }
}
