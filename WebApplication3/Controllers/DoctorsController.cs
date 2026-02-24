using Microsoft.AspNetCore.Mvc;
using ShopAPI.Models;
using ShopAPI.Services;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorService _service;

        public DoctorsController(DoctorService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Doctors>> Get() =>
            Ok(_service.GetAll());

        [HttpGet("{id}")]
        public ActionResult<Doctors> Get(int id)
        {
            var doctor = _service.GetById(id);
            return doctor == null ? NotFound() : Ok(doctor);
        }

        [HttpPost]
        public IActionResult Post(Doctors doctor)
        {
            _service.Add(doctor);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Doctors doctor)
        {
            _service.Update(doctor);
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