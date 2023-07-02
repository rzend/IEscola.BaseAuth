using IEscola.Auth.Models;
using IEscola.Auth.Repositories;
using IEscola.Auth.Services.Interfaces;
using IEscola.Core.Controllers;
using IEscola.Core.Notificacoes.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IEscola.Auth.Controllers
{
    [ApiController]
    public class AuthController : MainController
    {

        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly ICredentialsRepository _credentialsRepository;

        public AuthController(ITokenService tokenService,
            ICredentialsRepository credentialsRepository,
            IUserService userService,
            INotificador notificador) : base(notificador)
        {
            _tokenService = tokenService;
            _credentialsRepository = credentialsRepository;
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateAsync([FromBody] User model)
        {
            var user = await _userService.LoginAsync(model.Username, model.Password);

            if (user == null)
                return NotFound(new { message = "Usuário não encontrado" });

            var token = await _tokenService.GenerateTokenAsync(user);
            user.Password = string.Empty;

            var result = new
            {
                user = new
                {
                    username = user.Username,
                    role = user.Role
                },
                token = token
            };

            return SimpleResponse(result);
        }

        [HttpPost]
        [Route("generate-token")]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateTokenAsync([FromBody] Credentials credentialsRequest)
        {
            var credenciais = await _credentialsRepository.LoginAsync(credentialsRequest.ApiKey, credentialsRequest.Secret, credentialsRequest.CredentialsType);

            if (credenciais == null)
                return NotFound(new { message = "Credential não encontrada" });

            var token = _tokenService.GenerateToken(credentialsRequest);

            var result = new
            {
                apiKey = credentialsRequest.ApiKey,
                token = token
            };

            return SimpleResponse(result);
        }


        [HttpPost]
        [Route("validate-token")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateTokenAsync([FromBody] ValidateToken validadeTokenRequest)
        {
            var tokenValido = await Task.FromResult(_tokenService.ValidateToken(validadeTokenRequest));

            if (tokenValido.StatusCode == 200 || tokenValido.StatusCode == 400)
            {
                var result = new
                {
                    tokenValido = true
                };

                return SimpleResponse(result);
            }

            return SimpleResponseAuthorizedError(tokenValido.StatusCode);
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public IActionResult Anonymous()
        {
            return SimpleResponse("Anônimo");
        }

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public IActionResult Authenticated()
        {
            return SimpleResponse(string.Format("Autenticado - {0}", User.Identity.Name));
        }

        [HttpGet]
        [Route("peao")]
        [Authorize(Roles = "peao,coordenador,gerente")]
        public IActionResult Peao()
        {
            return SimpleResponse(string.Format("{0} - {1}", GetUserRole(User), User.Identity.Name));
        }

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "coordenador,gerente")]
        public IActionResult Manager()
        {
            return SimpleResponse(string.Format("{0} - {1}", GetUserRole(User), User.Identity.Name));
        }




        public static string GetUserRole(ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.Role);
            return claim?.Value;
        }
    }
}
