using OSM.Common;
using OSM.Service.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSM.Service.Manager.ServiceRequestManagement
{
    public interface IServiceRequestManager : IActionManager
    {
        IEnumerable<TenantServiceRequest> GetAllTenantServiceRequests();
    }
}
