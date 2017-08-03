using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OSM.Common;
using OSM.Service;
using System.Collections.Generic;

namespace OSM.WebCMS.Api
{
    [Produces("application/json")]
    [Route("api/ProductCategory")]
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryService _productCategoryService;
        private readonly ILogger _logger;
        private readonly IErrorService _errorService;

        public ProductCategoryController(IProductCategoryService productCategoryService, ILogger<ProductCategoryController> logger)
        {
            _productCategoryService = productCategoryService;
            _logger = logger;
        }

        public IActionResult GetById(int id)
        {
            _logger.LogInformation(LoggingEvents.GET_ITEM, "Getting item {ID}", id);
            var item = _productCategoryService.GetById(id);
            if (item == null)
            {
                _logger.LogWarning(LoggingEvents.GET_ITEM_NOTFOUND, "GetById({ID}) NOT FOUND", id);
                return NotFound();
            }
            return new ObjectResult(item);
        }


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
            var item = _productCategoryService.GetById(id);
            if (item == null)
            {
                NotFound();
            }
            _productCategoryService.Delete(id);
        }
    }
}
