using Microsoft.Extensions.Logging;
using OSM.Common;
using OSM.Data.Infrastructure;
using OSM.Model.Abstract;
using OSM.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
namespace OSM.Service.Manager.ServiceRequestManagement
{
    public class ServiceRequestManager : BusinessManager, IServiceRequestManager
    {
        IRepositoryBase _repository;
        ILogger<ServiceRequestManager> _logger;
        IUnitOfWork _unitOfWork;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }
        public ServiceRequestManager(IRepositoryBase repository, ILogger<ServiceRequestManager> logger,
        IUnitOfWork unitOfWork) : base()
        {
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public void Create(Auditable entity)
        {
            ServiceRequest serviceRequest = (ServiceRequest)entity;
            _logger.LogInformation("Creating record for {0}",
            this.GetType());
            _repository.Add<ServiceRequest>(serviceRequest);
            _logger.LogInformation("Record saved for {0}",
            this.GetType());
        }
        public void Delete(Auditable entity)
        { }
        public IEnumerable<Auditable> GetAll()
        {
            throw new NotImplementedException();
        }
        public void Update(Auditable entity)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<TenantServiceRequest> GetAllTenantServiceRequests()
        {
            var query = from tenants in _repository.All<Tenant>()
                        join serviceReqs in _repository.All<ServiceRequest>()
                        on tenants.ID equals serviceReqs.TenantID
                        join status in _repository.All<Status>()
                        on serviceReqs.StatusID equals status.ID
                        select new TenantServiceRequest()
                        {
                            TenantID = tenants.ID,
                            Description = serviceReqs.Description,
                            Email = tenants.Email,
                            EmployeeComments = serviceReqs.EmployeeComments,
                            Phone = tenants.Phone,
                            Status = status.Description,
                            TenantName = tenants.Name
                        };
            return query.ToList<TenantServiceRequest>();
        }
        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }
    }
}
