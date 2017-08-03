using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OSM.Model.Abstract
{
    public abstract class Auditable : IAuditable
    {
        public Auditable()
        {
            this.CreatedDate = DateTime.Now;
            this.UpdatedDate = DateTime.Now;
            this.State = (int)EntityState.New;
        }

        [MaxLength(250)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [MaxLength(250)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [MaxLength(250)]
        public string MetaKeyword { get; set; }

        [MaxLength(250)]
        public string MetaDescription { get; set; }

        [NotMapped]
        public int State { get; set; }

        public enum EntityState
        {
            New = 1, Update = 2, Delete = 3, Ignore = 4
        }
    }
}