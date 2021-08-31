using System;
using System.Threading.Tasks;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context) => _context = context;

        #region Methods  Register(User user, string password),Login(string username, string password), UserExists(string username), CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)

        #region async Task<ServiceResponse<int>> Register(User user, string password) --> registeres users? no same userName register User : error message is thrown when username is already registered

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var response = new ServiceResponse<int>(); // create a new service respense with with int as a the respoense 
            if (await UserExists(user.UserName))
            {
                response.Success = false;
                response.Message = $"{user.UserName} already exists sorry pick a new name ";
                return response;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            // not that in here the password is being passed from  the function. 
            //  passwordHash byte function is being created here 
            // as well as passwordSalt theese both values does not need to be created as out is on there. 
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            _context.Users.Add(user); // this adds the user
            await _context.SaveChangesAsync();

            response.Data = user.Id;
            return response;
        }

        #endregion

        #region Task<ServiceResponse<string>> Login(string username, string password) --> loggs in the user

        public Task<ServiceResponse<string>> Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        #endregion

        #region Task<bool> UserExists(string username) --> checks whether or not user UserExists.

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.UserName.ToLower().Equals(username.ToLower())))
            {
                return true;
            }

            return false;
        }

        #endregion

        // checks whehther or not same Id exists or not. 

        #region CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) --> creates passwordHash for the user

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            // out will be used to return value as reference type. it will be out thus does not need to be returned anything. 
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
                // Computes a Hash-based Message Authentication Code 
            {
                passwordSalt = hmac.Key; // secret cryptographic key is being set here. 
                passwordHash =
                    hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // creates hash based on password. 
            }
        }

        #endregion
    }

    #endregion
}