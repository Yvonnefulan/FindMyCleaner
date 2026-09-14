using FindMyCleaner.Model;
using FindMyCleaner.Services;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;

namespace FindMyCleaner.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    public class CleaningServicesController : ControllerBase
    {
        private readonly CleaningServicesService _cleaningServicesService;

        public CleaningServicesController(CleaningServicesService cleaningServicesService)
        {
            _cleaningServicesService = cleaningServicesService;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<List<CleaningServices>>> Get([FromQuery] string? suburb, [FromQuery] string? serviceType, [FromQuery] double? maxPrice)
        {
            var services = await _cleaningServicesService.GetAsync(suburb, serviceType, maxPrice);

            if (services.Count == 0)
            {
                return NotFound("No cleaning services found.");
            }

            return Ok(services.Select(service => new
            {
                service.Id,
                service.serviceProvider,
                service.serviceName,
                service.serviceType,
                service.suburb,
                service.priceFrom,
                service.minDurationHours,
                service.isAvailable,
                service.availableDay,
                service.createdDate,
                service.keywords
            }));
        }

        //for GET by Id。
        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<CleaningServices>> GetById(string id)
        {
            var service = await _cleaningServicesService.GetByIdAsync(id);

            if (service == null)
            {
                return NotFound("Cleaning service not found.");
            }

            return Ok(new
            {
                service.Id,
                service.serviceProvider,
                service.serviceName,
                service.serviceType,
                service.suburb,
                service.priceFrom,
                service.minDurationHours,
                service.isAvailable,
                service.availableDay,
                service.createdDate,
                service.keywords
            });
        }

        //POST add data
        [HttpPost]
        public async Task<IActionResult> Post(CleaningServices newService)
        {
            await _cleaningServicesService.CreateAsync(newService);

            return CreatedAtAction(nameof(GetById), new { id = newService.Id }, newService);
        }

        //PUT update data
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, CleaningServices updatedService)
        {
            var existingService = await _cleaningServicesService.GetByIdAsync(id);

            if (existingService == null)
            {
                return NotFound("Cleaning service not found.");
            }

            updatedService.Id = existingService.Id;

            await _cleaningServicesService.UpdateAsync(id, updatedService);

            return NoContent();
        }

        //Delete to remove data
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var service = await _cleaningServicesService.GetByIdAsync(id);

            if (service == null)
            {
                return NotFound("Cleaning service not found.");
            }

            await _cleaningServicesService.RemoveAsync(id);

            return NoContent();
        }

        //GET keyword search
        [HttpGet("search")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<List<CleaningServices>>> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest("Keyword is required.");
            }

            var services = await _cleaningServicesService.SearchByKeywordAsync(keyword);

            if (services.Count == 0)
            {
                return NotFound("No cleaning services found.");
            }

            return Ok(services.Select(service => new
            {
                service.Id,
                service.serviceProvider,
                service.serviceName,
                service.serviceType,
                service.suburb,
                service.priceFrom,
                service.minDurationHours,
                service.isAvailable,
                service.availableDay,
                service.createdDate,
                service.keywords
            }));
        }

        //GET Sorting Data
        [HttpGet("sort/price")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<List<CleaningServices>>> SortByPrice([FromQuery] string order = "asc")
        {
            var services = await _cleaningServicesService.GetSortedByPriceAsync(order);

            return Ok(services.Select(service => new
            {
                service.Id,
                service.serviceProvider,
                service.serviceName,
                service.serviceType,
                service.suburb,
                service.priceFrom,
                service.minDurationHours,
                service.isAvailable,
                service.availableDay,
                service.createdDate,
                service.keywords
            }));
        }

        //below 2.0
        [HttpGet]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<List<CleaningServices>>> GetV2([FromQuery] string? suburb, [FromQuery] string? serviceType, [FromQuery] double? maxPrice)
        {
            var services = await _cleaningServicesService.GetAsync(suburb, serviceType, maxPrice);

            if (services.Count == 0)
            {
                return NotFound("No cleaning services found.");
            }

            return Ok(services);
        }

        //for GET by Id。
        [HttpGet("{id}")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<CleaningServices>> GetByIdV2(string id)
        {
            var service = await _cleaningServicesService.GetByIdAsync(id);

            if (service == null)
            {
                return NotFound("Cleaning service not found.");
            }

            return Ok(service);
        }


        //GET keyword search
        [HttpGet("search")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<List<CleaningServices>>> SearchV2([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest("Keyword is required.");
            }

            var services = await _cleaningServicesService.SearchByKeywordAsync(keyword);

            if (services.Count == 0)
            {
                return NotFound("No cleaning services found.");
            }

            return Ok(services);
        }

        //GET Sorting Data
        [HttpGet("sort/price")]
        [MapToApiVersion("2.0")]
        public async Task<ActionResult<List<CleaningServices>>> SortByPriceV2([FromQuery] string order = "asc")
        {
            var services = await _cleaningServicesService.GetSortedByPriceAsync(order);

            return Ok(services);
        }



    }


}
