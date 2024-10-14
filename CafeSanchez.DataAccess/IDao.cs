using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeSanchez.DataAccess
{
    public interface IDao<TEntity> where TEntity : class
    {
        TEntity Create(TEntity entity);
        IEnumerable<TEntity> Read();
        TEntity Update(TEntity entity);
        bool Delete(TEntity entity);
    }
}
