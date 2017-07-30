using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OSM.WebCMS.Infrastructure.Core;

namespace OSM.WebCMS.Api
{
    [Produces("application/json")]
    [Route("api/ProductCategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        // GET: api/ProductCategory
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ProductCategory/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/ProductCategory
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/ProductCategory/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
