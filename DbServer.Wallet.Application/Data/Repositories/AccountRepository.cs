using DbServer.Wallet.Application.Data.Models.Entities;
using DbServer.Wallet.Application.Data.Repositories.Common;
using DbServer.Wallet.Application.Data.Repositories.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DbServer.Wallet.Application.Data.Repositories
{
    public class AccountRepository : RepositoryBase, IAccountRepository
    {
        public AccountRepository(SqlConnectionProvider connection) : base(connection)
        {
        }

        public async Task<Account> GetAsync(int agencyId, int accountNumber, SqlTransaction transaction = null)
        {
            using (var cmd = CreateCommand(transaction))
            {
                cmd.CommandText = "SELECT Password, CreatedDate, LastChangeDate, AccountId FROM Account WHERE AgencyId = @AgencyId AND AccountNumber = @AccountNumber AND Enabled = 1";

                cmd.Parameters.Add("@AgencyId", SqlDbType.Int).Value = agencyId;
                cmd.Parameters.Add("@AccountNumber", SqlDbType.Int).Value = accountNumber;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.HasRows && await reader.ReadAsync())
                    {
                        return new Account
                        {
                            AccountId = reader.GetInt64(3),
                            AccountNumber = accountNumber,
                            AgencyId = agencyId,
                            CreatedDate = reader.GetDateTime(1),
                            Enabled = true,
                            LastChangeDate = reader.GetDateTimeNullable(2),
                            Password = reader.GetString(0)
                        };
                    }

                    return null;
                }
            }
        }

        public async Task<Account> GetBydIdAsync(long id, SqlTransaction transaction = null)
        {
            using (var cmd = CreateCommand(transaction))
            {
                cmd.CommandText = "SELECT Password, CreatedDate, LastChangeDate, AgencyId, AccountNumber FROM Account WHERE AccountId = @AccountId AND Enabled = 1";

                cmd.Parameters.Add("@AccountId", SqlDbType.BigInt).Value = id;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.HasRows && await reader.ReadAsync())
                    {
                        return new Account
                        {
                            AccountNumber = reader.GetInt32(4),
                            AgencyId = reader.GetInt32(3),
                            CreatedDate = reader.GetDateTime(1),
                            Enabled = true,
                            LastChangeDate = reader.GetDateTimeNullable(2),
                            Password = reader.GetString(0)
                        };
                    }

                    return null;
                }
            }
        }

        public Task<Account> InsertAsync(Account entity, SqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public Task<Account> UpdateAsync(Account entity, SqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }
    }
}