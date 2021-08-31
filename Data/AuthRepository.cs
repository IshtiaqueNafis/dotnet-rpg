using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_rpg.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration; // gives acess to appsetting.json file 
        }

        #region Methods  Register(User user, string password),Login(string username, string password), UserExists(string username), CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)

        #region async Task<ServiceResponse<int>> Register(User user, string password) --> registeres users? no same userName register User : error message is thrown when username is already registered

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            var response =
                new ServiceResponse<int>(); // create a new service respense with with int as a the respoense 
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

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response = new ServiceResponse<string>();
            User user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username); // checl user name 
            if (user == null)
            {
                response.Success = false;
                response.Message = "User Not found";
            }
            else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Wrong password";
            }
            else
            {
                response.Data = CreateToken(user);
            }

            return response;
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

        #region VerifyPasswordHash --> verifies password to make sure userlooged in properly

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)

            #region Code Explanation

            /*
             *   private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
             * password --> willbe user input what ever the user provides
             * byte[] passwordHash--> this will be retrived from the database.
             * byte[] passwordSalt --> this will be retrived from the database. 
             */

            #endregion

        {
            using (HMACSHA512 hmac =
                new System.Security.Cryptography.HMACSHA512(
                    passwordSalt)) // this recivers a password salt  and creates HMACSHA512 with passwordSalt 
            {
                byte[] computeHash =
                    hmac.ComputeHash(
                        System.Text.Encoding.UTF8.GetBytes(password)); // create computedHashBased on password
                for (int i = 0; i < computeHash.Length; i++) //loop through computed hash 
                {
                    if (computeHash[i] != passwordHash[i]) // check whether or not both are euqal. 
                    {
                        return
                            false; // if one single character is wrong end the loop right away means user has entered the wrong password. 
                    }
                }

                return true;
            }
        }

        #endregion

        #region CreateToken

        private string CreateToken(User user) // get the user object. 
        {
            var claims = new List<Claim>
                // Claims are the user data and they are issued by a trusted source
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    // http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier: 2 2 --> nafis is the name of the user 
                    // notethat identifier is set here  based ClaimTypes.NameIdentifier,
                    new Claim(ClaimTypes.Name, user.UserName)
                    //The URI for a claim that specifies the name of an entity,
                    //http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name: nafis --> nafis is the name of the user when typed in 
                    // ClaimTypes.Name is the item type on here. 
                };
            foreach (var VARIABLE in claims)
            {
                Console.WriteLine(VARIABLE);
            }

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            #region code expalanation

            /*
             * SymmetricSecurityKey --> where only one key (a secret key) is used to both encrypt and decrypt electronic information
             * new SymmetricSecurityKey(  System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value))
             * --> creates a new security key but it is based on app.json key from app.settings value. 
             */

            #endregion

            Console.Write(key);
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            #region Code explanation

/*
 * SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
 * SigningCredentials creds --> Represents the cryptographic key and security algorithms that are used to generate a digital signature.
 *                          --> digital signature  highest level of assurance of a signer's identity
 *                          -->  SigningCredentials takes two parameter on constructor
 *                         --> key is the security key
 *                        --> SecurityAlgorithms.HmacSha512Signature --> is the algorithim used to create the digital siginituare. 
 */

            #endregion

            Console.Write(creds);


            var tokenDesciptor = new SecurityTokenDescriptor // gives acess to specific services.  
            {
                Subject = new ClaimsIdentity(claims), // creates a claim idenity based claims object in this case list 
                Expires = System.DateTime.Now.AddDays(1), // how many days is the expiry avilable. 
                SigningCredentials = creds // The credentials that are used to sign the token signs the user 
            };
            JwtSecurityTokenHandler
                tokenHandler = new JwtSecurityTokenHandler(); // designed for creating and validating Json Web Tokens
            SecurityToken token = tokenHandler.CreateToken(tokenDesciptor);
            //  SecurityToken--> It acts like an electronic key to access something
            //tokenHandler CreateToken(tokenDesciptor);  --> creates a secutiy contnet based on SecurityTokenDescriptor 
            return tokenHandler.WriteToken(token); // returns the item on a string format. 
        }

        #endregion

        #endregion
    }
}