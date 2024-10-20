using CafeSanchez.DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeSanchez.DataAccess
{
    public static class DaoFactory
    {
        public static TDao Create<TDao>(string connectionString) where TDao : class
        {
            return typeof(TDao) switch
            {
                var dao when dao == typeof(IProductDao) => new ProductDao(connectionString) as TDao,
                _ => throw new DaoException("Unknown DAO")
            } ?? throw new DaoException($"A problem occurred creating a DAO for {nameof(TDao)}");
        }
    }
}
