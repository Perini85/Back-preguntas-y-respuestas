using BackCursoUdemy2.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;


namespace BackCursoUdemy2.Utils
{
    public class JwtConfigurator
    {

        public static string GetToken(Usuario userinfo, IConfiguration config)
        {

            
                string SecretKey = config["Jwt:SecretKey"];
                string Issuer = config["Jwt:Issuer"];
                string Audience = config["Jwt:Audience"];

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
               {
                new Claim(JwtRegisteredClaimNames.Sub, userinfo.nombreUsuario),
                new Claim("idUsuario",userinfo.Id.ToString())
            };

                var token = new JwtSecurityToken(
                    issuer: Issuer,
                    audience: Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: credentials
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

         public static int GetTokenIdUsuario(ClaimsIdentity identity)
        {
            if(identity != null)
            {

                IEnumerable<Claim> claims = identity.Claims;
                foreach( var claim in claims)
                {
                    if(claim.Type == "idUsuario")
                    {
                        return int.Parse(claim.Value);
                    }
                }

            }
            return  0;


        }

        }
            
     }      
             
   
  



    

