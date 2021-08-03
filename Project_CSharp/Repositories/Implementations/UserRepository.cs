using Project_CSharp.DTOs.Request;
using Project_CSharp.Models;
using Project_CSharp.Repositories.Interfaces;
using Project_CSharp.Services;
using Project_CSharp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace Project_CSharp.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        public static List<User> Users = new List<User>();


        private readonly IFileService fileService;
        private readonly IAuthService authService;

        public UserRepository(IFileService fileService, IAuthService authService)
        {
            this.authService = authService;
            this.fileService = fileService;
        }

        public async Task<Answer> CreateAsync(AuthUser authUser)
        {
            var checkUser = Users.Find(user => user.Email.Equals(authUser.Email));
            if (checkUser != null)
                return Answer.UserIsAlreadyRegistered;

            var hashedPasswordAndSalt = AuthService.GetHashedPasswordAndSalt(authUser.Password);
            var user = new User
            {
                Email = authUser.Email,
                Password = hashedPasswordAndSalt.Item1,
                Salt = hashedPasswordAndSalt.Item2,
                RefreshToken = Guid.NewGuid().ToString(),
                RefreshTokenExpiration = DateTime.Now.AddHours(12)
            };

            Users.Add(user);
            await fileService.WriteUserToFileAsync(user);

            return Answer.Ok;
        }

        public async Task<AuthHelper> LoginAsync(AuthUser authUser)
        {
            var wantedUser = Users.Find(user => user.Email.Equals(authUser.Email));
            if (wantedUser == null)
                return new AuthHelper(Answer.UserNotFound);

            if (wantedUser.Password != authService.GetUnhashedPassword(authUser.Password, wantedUser.Salt))
                return new AuthHelper(Answer.BadPassword); 

            var token = authService.GetToken();

            wantedUser.RefreshToken = Guid.NewGuid().ToString();
            wantedUser.RefreshTokenExpiration = DateTime.Now.AddHours(12);

            await fileService.OverwriteFileAsync();

            return new AuthHelper(Answer.Ok, new JwtSecurityTokenHandler().WriteToken(token).ToString(), wantedUser.RefreshToken);
        }

        public async Task<AuthHelper> UpdateAccessTokenAsync(string refreshToken)
        {
            var wantedUser = Users.Find(user => user.RefreshToken == refreshToken);
            if (wantedUser == null)
                return new AuthHelper(Answer.UserNotFound);

            if (wantedUser.RefreshTokenExpiration < DateTime.Now)
                return new AuthHelper(Answer.RefreshTokenIsExpired);

            var token = authService.GetToken();

            wantedUser.RefreshToken = Guid.NewGuid().ToString();
            wantedUser.RefreshTokenExpiration = DateTime.Now.AddHours(12);

            await fileService.OverwriteFileAsync();

            return new AuthHelper(Answer.Ok, new JwtSecurityTokenHandler().WriteToken(token), wantedUser.RefreshToken);
        }
    }
}
