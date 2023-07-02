using IEscola.Auth.Models;
using IEscola.Auth.Services.Interfaces;
using IEscola.Core.Extensions;
using IEscola.Core.Notificacoes.Interfaces;
using IEscola.Core.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IEscola.Auth.Services
{
    public class TokenService : ServiceBase, ITokenService
    {
        private const string CLAIM_NAME_APIKEY = "api-key";
        private readonly Settings _settings;

        public TokenService(IOptions<Settings> settings,
            INotificador notificador) : base(notificador)
        {
            _settings = settings.Value;
        }

        public async Task<string> GenerateTokenAsync(User user)
        {
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_settings.Secret);
            //var tokenDescriptor = new SecurityTokenDescriptor { 

            //    Subject = new ClaimsIdentity(new Claim[] { 
            //        new Claim(ClaimTypes.Name, user.Username),
            //        new Claim(ClaimTypes.Role, user.Role)
            //    }),
            //    Expires = DateTime.UtcNow.AddHours(2),
            //    SigningCredentials = new SigningCredentials(
            //            new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};

            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //return await Task.FromResult(tokenHandler.WriteToken(token));

            return "";
        }

        public string GenerateToken(Credentials credentials)
        {
            var credentialsSettings = GetCredentialsSettings(credentials);

            if (credentialsSettings is null)
            {
                NotificarErro("Credenciais não encontradas");
                return default;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(credentialsSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(GetClaims(credentialsSettings)),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // Microsoft Identity
        private IEnumerable<Claim> GetClaims(Api credentialsSettings)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(CLAIM_NAME_APIKEY, credentialsSettings.ApiKey));
            claims.Add(new Claim("scopes", "jsakdjlasj, saklsajdjsa, adskjaodowqpipoqw, aksjdklasjdkjal"));
            return claims;
        }

        private Api GetCredentialsSettings(Credentials credentials)
        {
            return _settings.Apis.FirstOrDefault(a =>
                                                    a.ApiKey == credentials.ApiKey &&
                                                    a.Secret == credentials.Secret &&
                                                    a.CredentialType == credentials.CredentialsType);
        }

        public ValidadeTokenResponse ValidateToken(ValidateToken validadeTokenRequest)
        {
            if (validadeTokenRequest == null || string.IsNullOrWhiteSpace(validadeTokenRequest.Token) || string.IsNullOrWhiteSpace(validadeTokenRequest.ApiKey))
            {
                NotificarErro("ValidadeTokenRequest inválido");
                return new ValidadeTokenResponse(400);
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            if (!tokenHandler.CanReadToken(validadeTokenRequest.Token.ToCleanToken()))
            {
                NotificarErro("ValidadeTokenRequest inválido");
                return new ValidadeTokenResponse(400);
            }

            var jwt = tokenHandler.ReadJwtToken(validadeTokenRequest.Token.ToCleanToken());

            var apiKey = jwt.Claims.FirstOrDefault(x => x.Type == CLAIM_NAME_APIKEY)?.Value;
            var role = jwt.Claims.FirstOrDefault(x => x.Type == "role")?.Value;
            var dataExpiracao = DateTimeOffset.FromUnixTimeSeconds(long.Parse(jwt.Claims.FirstOrDefault(x => x.Type == "exp")?.Value)).UtcDateTime;

            if (dataExpiracao < DateTime.UtcNow)
            {
                NotificarErro("Token expirado.");
                return new ValidadeTokenResponse(403);
            }

            return new ValidadeTokenResponse(200);
        }
    }
}
