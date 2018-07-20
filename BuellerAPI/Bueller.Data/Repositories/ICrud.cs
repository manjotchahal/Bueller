using Bueller.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bueller.Data.Repositories
{
    public interface ICrud<T> where T : BaseModel
    {
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        //void Delete(T entity);
        void Delete(object id);
        IQueryable<T> Table { get; }
    }
}
