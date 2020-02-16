using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

/// <summary>
/// This class creates and validates Jason Web Tokens (JWT). 
/// It requires installation of the System.IdentityModel.Tokens.Jwt package. 
/// The process uses symmetric encryption where a single key is used to encrypt 
/// and decrypt information passed between applications. 
/// </summary>
namespace LoginServiceDemo.Models
{
    public class TokenManager
    {
        // Token genaration method where a token is created with the username imbedded and a string version returned
        public static string GenerateToken(string username, string secret)
        {
            /// <summary>
            /// Token genaration method where a token is created and a string version of it returned.
            /// Creates the descriptor object, which represents the main content of the JWT, 
            /// such as the claims, the expiration date and the signing information. 
            /// The token is created and a string version of it is returned. In this example
            /// only the username is added but the list of claim types that can be added is 
            /// large. 
            /// It is also possible to add custom data to the token post creation by accessing the JWT
            /// payload property with code like: token.Payload["NicksFavouriteFood"] = "Huel";
            /// </summary>

            byte[] key = Convert.FromBase64String(secret);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.Name, username)}),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        } 

    }

}