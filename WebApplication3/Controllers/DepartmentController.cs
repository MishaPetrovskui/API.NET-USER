using Microsoft.AspNetCore.Mvc;
using ShopAPI.Models;
using ShopAPI.Services;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentsService _service;
        public DepartmentController(DepartmentsService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult<List<Departments>> GetAll()
        {
            return Ok(_service.GetAll());
        }
        [HttpGet("{id}")]
        public ActionResult<Departments> GetById(int id)
        {
            var specialization = _service.GetById(id);
            if (specialization == null)
                return NotFound();
            return Ok(specialization);
        }
        [HttpPost]
        public ActionResult Add(Departments specialization)
        {
            _service.Add(specialization);
            return Ok();
        }
        [HttpPut]
        public ActionResult Update(Departments specialization)
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
