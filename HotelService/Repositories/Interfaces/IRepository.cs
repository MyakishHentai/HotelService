using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelService.Repositories.Interfaces
{
    public interface IRepository<TModel, in TKeyType> : IDisposable
        where TModel : class
    {
        IQueryable<TModel> GetAll();
        TModel Get(TKeyType id);
        void Create(TModel item);
        void Update(TModel item);
        void Delete(TKeyType id);
        Task Save();
    }
}
