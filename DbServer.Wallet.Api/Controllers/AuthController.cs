using DbServer.Wallet.Api.ViewModels.Auth;
using DbServer.Wallet.Api.ViewModels.Shared;
using DbServer.Wallet.Application;
using DbServer.Wallet.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DbServer.Wallet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AuthController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorViewModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponsePostAuthViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody]PostAuthViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var account = await _accountService.ValidateAsync(model);

            if (account == null)
                return NotFound(new ErrorViewModel("Usuário não encontrado"));

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sid, account.AccountId.ToString())
            };

            var key = new SymmetricSecurityKey(AppSettings.JwtKey);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.Now;

            var token = new JwtSecurityToken(
              issuer: "DbServer",
              audience: "DbServer",
              claims: claims,
              expires: now.AddHours(1),
              signingCredentials: creds);

            return Ok(new ResponsePostAuthViewModel
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Validity = 60
            });
        }
    }
}