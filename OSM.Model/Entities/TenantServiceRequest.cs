using OSM.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSM.Common
{
    public class TenantServiceRequest : Auditable
    {
        public string Description
        {
            get; set;
        }
        public string EmployeeComments { get; set; }
        public string Status
        {
            get; set;
        }
        public long TenantID { get; set; }
        public string TenantName
        {
            get; set;
        }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
