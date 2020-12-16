﻿using BLL_HWTA.Interfases;
using BLL_HWTA.Models;
using BLL_HWTA.TokenApp;
using DAL_HWTA;
using DAL_HWTA.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL_HWTA.Concrete
{
    public class UsersManager : IUsersManager
    {
        private readonly HwtaDbContext _dbContext;
        public UsersManager(HwtaDbContext hwtaDbContext)
        {
            _dbContext = hwtaDbContext;
        }

        public async Task<bool> AddUserAsync(string email, string password, string firstName, string lastName)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                _dbContext.Users
                    .Add(
                    new User
                    {
                        Email = email,
                        PasswordHash = ComputeSHA512(password),
                        Role = Role.User,
                        FirstName = firstName,
                        LastName = lastName
                    });
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<ClaimsIdentity> GetIdentityAsync(string email, string password)
        {

            var user = await LoginAsync(email, password);

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    
    public string GetToken(ClaimsIdentity identity)
    {
        var now = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }




    private async Task<User> LoginAsync(string email, string password)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Email == email.ToLower());

        if (user == null)
        {
            return null;
        }

        var output = ComputeSHA512(password);

        if (output.SequenceEqual(user.PasswordHash))
        {
            return user;
        }

        return null;
    }

    private byte[] ComputeSHA512(string password)
    {
        using SHA512 sha = SHA512.Create();

        return sha.ComputeHash(Encoding.UTF8.GetBytes(password));

    }

}
}
