using Microsoft.Extensions.Logging;
using OSM.Common;
using OSM.Data.Infrastructure;
using OSM.Model.Abstract;
using OSM.Service.Core;
using OSM.Service.Manager.ServiceRequestManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSM.Service.Manager.TeamManagement
{
    public class TenantManager : BusinessManager, ITenantManager
    {
        IRepositoryBase _repository;
        ILogger<TenantManager> _logger;
        IUnitOfWork _unitOfWork;
        IServiceRequestManager _serviceRequestManager;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }
        public TenantManager(IRepositoryBase repository, ILogger<TenantManager> logger, IUnitOfWork unitOfWork,
        IServiceRequestManager serviceRequestManager) : base()
        {
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _serviceRequestManager = serviceRequestManager;
        }
        public virtual Tenant GetTenant(long tenantID)
        {
            try
            {
                _logger.LogInformation(LoggingEvents.GET_ITEM,
                "The tenant Id is " + tenantID);
                return _repository.All<Tenant>().Where(i => i.ID ==
                tenantID).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Create(Auditable entity)
        {
            Tenant tenant = (Tenant)entity;
            _logger.LogInformation("Creating record for {0}",
            this.GetType());
            _repository.Add<Tenant>(tenant);
            SaveChanges();
            _logger.LogInformation("Record saved for {0}",
            this.GetType());
        }
        public void Update(Auditable entity)
        {
            Tenant tenant = (Tenant)entity;
            _logger.LogInformation("Updating record for {0}",
            this.GetType());
            _repository.Update<Tenant>(tenant);
            SaveChanges();
            _logger.LogInformation("Record saved for {0}",
            this.GetType());
        }
        public void Delete(Auditable entity)
        {
            Tenant tenant = (Tenant)entity;
            _logger.LogInformation("Updating record for {0}",
            this.GetType());
            _repository.Delete<Tenant>(tenant);
            SaveChanges();
            _logger.LogInformation("Record deleted for {0}",
            this.GetType());
        }
        IEnumerable<Auditable> IActionManager.GetAll()
        {
            return _repository.All<Tenant>().ToList<Tenant>();
        }
        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }
    }
}
