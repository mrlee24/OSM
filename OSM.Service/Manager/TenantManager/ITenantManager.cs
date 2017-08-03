using OSM.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSM.Service.Manager.TeamManagement
{
    public interface ITenantManager
    {
        Tenant GetTenant(long tenantID);
    }
}
