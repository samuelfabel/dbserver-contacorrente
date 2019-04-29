using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DbServer.Wallet.Application.Data.Repositories.Interfaces
{
    /// <summary>
    /// Interface base dos repositories
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        Task<T> GetBydIdAsync(long id, SqlTransaction transaction = null);

        Task<T> InsertAsync(T entity, SqlTransaction transaction = null);

        Task<T> UpdateAsync(T entity, SqlTransaction transaction = null);

        SqlTransaction BeginTransaction();
    }
}