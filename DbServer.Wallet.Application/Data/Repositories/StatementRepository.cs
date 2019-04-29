using DbServer.Wallet.Application.Data.Models.Entities;
using DbServer.Wallet.Application.Data.Repositories.Common;
using DbServer.Wallet.Application.Data.Repositories.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DbServer.Wallet.Application.Data.Repositories
{
    public class StatementRepository : RepositoryBase, IStatementRepository
    {
        public StatementRepository(SqlConnectionProvider connection) : base(connection)
        {
        }

        public Task<Statement> GetBydIdAsync(long id, SqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }

        public async Task<Statement> InsertAsync(Statement entity, SqlTransaction transaction = null)
        {
            using (var cmd = CreateCommand(transaction))
            {
                cmd.CommandText = "INSERT INTO Statement (AccountId, TransactionTypeId, Amount, CreatedDate) VALUES (@AccountId, @TransactionTypeId, @Amount, @CreatedDate)";

                cmd.Parameters.Add("@AccountId", SqlDbType.BigInt).Value = entity.AccountId;
                cmd.Parameters.Add("@TransactionTypeId", SqlDbType.TinyInt).Value = entity.TransactionTypeId;
                cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = entity.Amount;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = entity.CreatedDate;

                await cmd.ExecuteNonQueryAsync();

                return entity;
            }
        }

        public Task<Statement> UpdateAsync(Statement entity, SqlTransaction transaction = null)
        {
            throw new NotImplementedException();
        }
    }
}