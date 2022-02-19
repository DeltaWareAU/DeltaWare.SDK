using System.Linq;

namespace DeltaWare.SDK.EntityFrameworkCore.StoredProcedure
{
    public interface IStoredProcedureExecuter<out TEntity> where TEntity : class
    {
        /// <summary>
        /// Executes the stored procedure.
        /// </summary>
        IQueryable<TEntity> Execute();
    }
}