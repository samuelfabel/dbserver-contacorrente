using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DbServer.Wallet.Api
{
    public static class ClaimsPrincipalExtension
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="claimsPrincipal"></param>
        /// <returns></returns>
        public static long GetAccountId(this ClaimsPrincipal claimsPrincipal)
        {
            return Convert.ToInt64(claimsPrincipal.FindFirst(JwtRegisteredClaimNames.Sid)?.Value);
        }
    }
}