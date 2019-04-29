using DbServer.Wallet.Application.Data.Models.Entities;
using DbServer.Wallet.Application.Data.Repositories.Interfaces;
using DbServer.Wallet.Application.Security;
using DbServer.Wallet.Application.Services.Model;
using System.Threading.Tasks;

namespace DbServer.Wallet.Application.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly PasswordSecurity _passwordSecurity;

        public AccountService(IAccountRepository accountRepository, PasswordSecurity passwordSecurity)
        {
            _accountRepository = accountRepository;
            _passwordSecurity = passwordSecurity;
        }

        /// <summary>
        /// Método responsável por validar uma conta
        /// </summary>
        /// <param name="agencyId">Id da agencia</param>
        /// <param name="accountNumber">Número da conta</param>
        /// <param name="password">Senha descriptografada</param>
        /// <returns>Sucesso da validação</returns>
        public async Task<Account> ValidateAsync(IValidateAccount validateAccount)
        {
            var account = await _accountRepository.GetAsync(validateAccount.Agency, validateAccount.Account);

            if (account == null)
                return null;

            if (_passwordSecurity.Verify(validateAccount.Password, account.Password))
                return account;

            return null;
        }
    }
}