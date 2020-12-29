using IntegrPoc.Api.Models;
using IntegrPoc.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace IntegrPoc.Api.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet("{id:Guid}")]
        public IActionResult Get(Guid id)
        {
            var customer = _service.Get(id);

            if (customer != null)
            {
                return Ok(customer);
            }

            return StatusCode(422);
        }

        [HttpPost("")]
        public IActionResult Register([FromBody] CustomerViewModel model)
        {
            _service.Register(model);
            return CreatedAtAction("Register", new Response { Message = "Customer registered" });
        }

        [HttpPut("")]
        public IActionResult Update([FromBody] CustomerViewModel model)
        {
            _service.Update(model);
            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult Delete(Guid id)
        {
            _service.Remove(id);
            return NoContent();
        }
    }
}
