using DbServer.Wallet.Application.Data.Models.Entities;
using DbServer.Wallet.Application.Data.Repositories.Common;
using DbServer.Wallet.Application.Data.Repositories.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DbServer.Wallet.Application.Data.Repositories
{
    public class BalanceRepository : RepositoryBase, IBalanceRepository
    {
        public BalanceRepository(SqlConnectionProvider connection) : base(connection)
        {
        }

        public async Task<Balance> GetBydIdAsync(long id, SqlTransaction transaction = null)
        {
            using (var cmd = CreateCommand(transaction))
            {
                cmd.CommandText = "SELECT CurrentValue, CreatedDate, LastChangeDate FROM Balance WHERE AccountId = @AccountId";

                cmd.Parameters.Add("@AccountId", SqlDbType.BigInt).Value = id;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.HasRows && await reader.ReadAsync())
                    {
                        return new Balance
                        {
                            AccountId = id,
                            CreatedDate = reader.GetDateTime(1),
                            CurrentValue = reader.GetDecimal(0),
                            Enabled = true,
                            LastChangeDate = reader.GetDateTimeNullable(2)
                        };
                    }

                    return await InsertAsync(new Balance { AccountId = id, CreatedDate = DateTime.Now, Enabled = true }, transaction);
                }
            }
        }

        public async Task<Balance> InsertAsync(Balance entity, SqlTransaction transaction = null)
        {
            using (var cmd = CreateCommand(transaction))
            {
                cmd.CommandText = "INSERT INTO Balance (AccountId, CurrentValue, CreatedDate) VALUES (@AccountId, @CurrentValue, @CreatedDate)";

                cmd.Parameters.Add("@AccountId", SqlDbType.BigInt).Value = entity.AccountId;
                cmd.Parameters.Add("@CurrentValue", SqlDbType.Decimal).Value = entity.CurrentValue;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = entity.CreatedDate;

                await cmd.ExecuteNonQueryAsync();

                return entity;
            }
        }

        public async Task<Balance> UpdateAsync(Balance entity, SqlTransaction transaction = null)
        {
            using (var cmd = CreateCommand(transaction))
            {
                cmd.CommandText = "UPDATE Balance SET CurrentValue = @CurrentValue, LastChangeDate = @LastChangeDate WHERE AccountId = @AccountId";

                cmd.Parameters.Add("@AccountId", SqlDbType.BigInt).Value = entity.AccountId;
                cmd.Parameters.Add("@CurrentValue", SqlDbType.Decimal).Value = entity.CurrentValue;
                cmd.Parameters.Add("@LastChangeDate", SqlDbType.DateTime).Value = entity.LastChangeDate;

                await cmd.ExecuteNonQueryAsync();

                return entity;
            }
        }
    }
}