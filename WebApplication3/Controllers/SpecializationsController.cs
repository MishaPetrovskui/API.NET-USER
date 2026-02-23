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
        public ActionResult<List<Specializations>> GetAll()
        {
            return Ok(_service.GetAll());
        }
        [HttpGet("{id}")]
        public ActionResult<Specializations> GetById(int id)
        {
            var specialization = _service.GetById(id);
            if (specialization == null)
                return NotFound();
            return Ok(specialization);
        }
        [HttpPost]
        public ActionResult Add([FromBody] Specializations specialization)
        {
            _service.Add(specialization);
            return Ok();
        }
        [HttpPut]
        public ActionResult Update([FromBody] Specializations specialization)
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