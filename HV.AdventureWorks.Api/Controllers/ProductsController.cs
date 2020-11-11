using Microsoft.AspNetCore.Mvc;
using HV.AdventureWorks.Services.Interfaces;
using HV.AdventureWorks.Services.Models;

namespace HV.AdventureWorks.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Get()
        {
            var products = _productsService.GetAll();

            return Ok(products);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get(int id)
        {
            var product = _productsService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody] Product product)
        {
            var savedProduct = _productsService.Create(product);

            return Ok(savedProduct);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(int id, [FromBody] Product product)
        {
            product.Id = id;
            var savedProduct = _productsService.Update(product);

            return Ok(savedProduct);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            _productsService.Delete(id);

            return Ok();
        }
    }
}
