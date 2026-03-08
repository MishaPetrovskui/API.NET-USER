using Microsoft.AspNetCore.Mvc;
using ShopAPI.DTOs;
using ShopAPI.Models;
using ShopAPI.Services;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("api/specializations")]
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

        [HttpGet("with-doctor-count")]
        public ActionResult<List<SpecializationWithCountDto>> GetWithDoctorCount()
        {
            var result = _service.GetWithDoctorCount();
            if (result == null || !result.Any())
                return NotFound("No specializations found.");

            return Ok(result);
        }
    }
}
