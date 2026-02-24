using Microsoft.AspNetCore.Mvc;
using ShopAPI.Models;
using ShopAPI.Services;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorsSpecializationsController : ControllerBase
    {
        private readonly DoctorsSpecializationService _service;

        public DoctorsSpecializationsController(DoctorsSpecializationService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<DoctorsSpecializations>> Get() =>
            Ok(_service.GetAll());

        [HttpGet("{id}")]
        public ActionResult<DoctorsSpecializations> Get(int id)
        {
            var item = _service.GetById(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public IActionResult Post(DoctorsSpecializations item)
        {
            _service.Add(item);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(DoctorsSpecializations item)
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