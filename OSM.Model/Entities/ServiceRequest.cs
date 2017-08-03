using OSM.Model.Abstract;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSM.Common
{
    [Description("To store Service Requests submitted by Tenants")]
    [Table("ServiceRequest")]
    public class ServiceRequest : Auditable
    {  
        [Key]
        public long ID { get; set; }
        public long TenantID { get; set; }
        [ForeignKey("TenantID")]
        public virtual Tenant Tenant { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [MaxLength(300)]
        public string EmployeeComments { get; set; }
        public int StatusID  { get; set; }
        [ForeignKey("StatusID")]
        public virtual Status Status { get; set; } }
}
