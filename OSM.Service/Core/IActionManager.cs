using OSM.Data.Infrastructure;
using OSM.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSM.Service.Core
{
    public interface IActionManager
    {
        void Create(Auditable entity);
        void Update(Auditable entity);
        void Delete(Auditable entity);
        IEnumerable<Auditable> GetAll();
        IUnitOfWork UnitOfWork { get; }
        void SaveChanges();
    }
}
