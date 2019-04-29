using DbServer.Wallet.Application.Data.Models.Entities;
using DbServer.Wallet.Application.Data.Repositories.Interfaces;
using DbServer.Wallet.Application.Enums;
using DbServer.Wallet.Application.Services.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace DbServer.Wallet.Application.Services
{
    public class WalletService
    {
        private readonly IStatementRepository _statementRepository;
        private readonly IBalanceRepository _balanceRepository;
        private readonly IAccountRepository _accountRepository;

        public WalletService(IStatementRepository statementRepository, IBalanceRepository balanceRepository, IAccountRepository accountRepository)
        {
            _statementRepository = statementRepository;
            _balanceRepository = balanceRepository;
            _accountRepository = accountRepository;
        }

        public async Task<(int, string)> CreateTransferAsync(long accountId, ITransfer transfer)
        {
            if (transfer.Amount <= 0)
                return (StatusCodes.Status400BadRequest, "Valor de transferência invalido.");

            using (var transaction = _accountRepository.BeginTransaction())
            {
                try
                {
                    var account = await _accountRepository.GetBydIdAsync(accountId, transaction);
                    var destinationAccount = await _accountRepository.GetAsync(transfer.DestinationAgency, transfer.DestinationAccount, transaction);

                    if (account == null)
                        return (StatusCodes.Status403Forbidden, "Usuário não encontrado.");

                    if (destinationAccount == null)
                        return (StatusCodes.Status404NotFound, "Usuário de destino inválido.");

                    var balance = await _balanceRepository.GetBydIdAsync(accountId, transaction);
                    var destinationBalance = await _balanceRepository.GetBydIdAsync(destinationAccount.AccountId, transaction);

                    if (balance.CurrentValue < transfer.Amount)
                        return (StatusCodes.Status400BadRequest, "Saldo insuficiente.");

                    var date = DateTime.Now;

                    balance.CurrentValue -= transfer.Amount;
                    balance.LastChangeDate = date;
                    destinationBalance.CurrentValue -= transfer.Amount;
                    destinationBalance.LastChangeDate = date;

                    await _balanceRepository.UpdateAsync(balance, transaction);
                    await _balanceRepository.UpdateAsync(destinationBalance, transaction);

                    await _statementRepository.InsertAsync(new Statement
                    {
                        AccountId = accountId,
                        Amount = -transfer.Amount,
                        CreatedDate = date,
                        Enabled = true,
                        TransactionTypeId = (byte)TransactionTypeEnum.DebitTransfer
                    }, transaction);

                    await _statementRepository.InsertAsync(new Statement
                    {
                        AccountId = destinationAccount.AccountId,
                        Amount = transfer.Amount,
                        CreatedDate = date,
                        Enabled = true,
                        TransactionTypeId = (byte)TransactionTypeEnum.CreditTransfer
                    }, transaction);

                    transaction.Commit();

                    return (StatusCodes.Status200OK, null);
                }
                catch
                {
                    transaction.Rollback();

                    throw;
                }
            }
        }
    }
}