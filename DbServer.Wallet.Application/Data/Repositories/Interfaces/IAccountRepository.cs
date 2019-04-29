using DbServer.Wallet.Application.Data.Models.Entities;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DbServer.Wallet.Application.Data.Repositories.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetAsync(int agencyId, int accountNumber, SqlTransaction transaction = null);
    }
}