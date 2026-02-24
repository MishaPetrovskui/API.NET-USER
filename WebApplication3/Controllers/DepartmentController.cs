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
        public ActionResult<List<Departments>> Get() =>
            Ok(_service.GetAll());

        [HttpGet("{id}")]
        public ActionResult<Departments> Get(int id)
        {
            var item = _service.GetById(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public IActionResult Post(Departments department)
        {
            _service.Add(department);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Departments department)
        {
            _service.Update(department);
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