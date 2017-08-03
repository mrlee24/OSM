using OSM.Service.Manager.ServiceRequestManagement;
using OSM.Service.Manager.TeamManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSM.Service.Core
{
    public class BusinessManagerFactory
    {
        IServiceRequestManager _serviceRequestManager;
        ITenantManager _tenantManager;
        public BusinessManagerFactory(IServiceRequestManager serviceRequestManager = null, ITenantManager tenantManager = null)
        {
            _serviceRequestManager = serviceRequestManager;
            _tenantManager = tenantManager;
        }
        public IServiceRequestManager GetServiceRequestManager()
        {
            return _serviceRequestManager;
        }
        public ITenantManager GetTenantManager()
        {
            return _tenantManager;
        }
    }
}
