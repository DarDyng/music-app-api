﻿using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusicAppApi.Core.Constants;
using MusicAppApi.Models.Configurations;
using MusicAppApi.Models.DbModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MusicAppApi.Core.Interfaces;

namespace MusicAppApi.Core.Services
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly JWTConfiguration _jwtConfiguration;

        public JwtGenerator(IOptions<JWTConfiguration> options)
        {
            this._jwtConfiguration = options.Value;
        }

        public string GenerateToken(string email, string userName, string userId)
        {
            var authSigngingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.AccessTokenSecret));
            var tokenHandler = new JwtSecurityTokenHandler();

            var authClaims = new List<Claim>
            {
                new Claim(AuthConstants.ClaimNames.Id, userId.ToString()),
                new Claim(ClaimTypes.Role, AuthConstants.UserRoles.User),
                new Claim(ClaimTypes.Email, email.ToString()),
                new Claim(ClaimTypes.Name, userName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _jwtConfiguration.Issuer,
                audience: _jwtConfiguration.Audience,
                expires: DateTime.Now.AddMinutes(_jwtConfiguration.AccessTokenExpirationMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigngingKey, SecurityAlgorithms.HmacSha256)
            );

            return tokenHandler.WriteToken(token);
        }
    }
}
