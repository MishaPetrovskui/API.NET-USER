using Microsoft.AspNetCore.Mvc;
using ShopAPI.Models;
using ShopAPI.Services;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecializationsController : ControllerBase
    {
        private readonly SpecializationService _service;

        public SpecializationsController(SpecializationService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Specializations>> Get() =>
            Ok(_service.GetAll());

        [HttpGet("{id}")]
        public ActionResult<Specializations> Get(int id)
        {
            var item = _service.GetById(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public IActionResult Post(Specializations item)
        {
            _service.Add(item);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Specializations item)
        {
            _service.Update(item);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}