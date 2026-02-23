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
        public ActionResult<List<DoctorsSpecializations>> GetAll()
        {
            return Ok(_service.GetAll());
        }
        [HttpGet("{id}")]
        public ActionResult<DoctorsSpecializations> GetById(int id)
        {
            var specialization = _service.GetById(id);
            if (specialization == null)
                return NotFound();
            return Ok(specialization);
        }
        [HttpPost]
        public ActionResult Add([FromBody] DoctorsSpecializations specialization)
        {
            _service.Add(specialization);
            return Ok();
        }
        [HttpPut]
        public ActionResult Update([FromBody] DoctorsSpecializations specialization)
        {
            _service.Update(specialization);
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }
    }
}