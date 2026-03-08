using Microsoft.AspNetCore.Mvc;
using ShopAPI.DTOs;
using ShopAPI.Models;
using ShopAPI.Services;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentsService _service;
        private readonly DoctorService _doctorService;

        public DepartmentController(DepartmentsService service, DoctorService doctorService)
        {
            _service = service;
            _doctorService = doctorService;
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

        [HttpGet("statistics")]
        public ActionResult<List<DepartmentStatisticsDto>> GetStatistics()
        {
            var result = _service.GetStatistics();
            if (result == null || !result.Any())
                return NotFound("No departments or doctors found.");

            return Ok(result);
        }

        [HttpGet("{departmentId}/above-average")]
        public ActionResult<List<DoctorDto>> GetAboveAverage(int departmentId)
        {
            if (departmentId <= 0)
                return BadRequest("DepartmentId must be greater than 0.");

            var department = _service.GetById(departmentId);
            if (department == null)
                return NotFound("Department not found.");

            var doctors = _doctorService.GetAboveAverageInDepartment(departmentId);
            if (doctors == null || !doctors.Any())
                return NotFound("No doctors found in the specified department.");

            return Ok(doctors);
        }

        [HttpGet("with-specializations")]
        public ActionResult<List<DepartmentWithSpecializationsDto>> GetWithSpecializations()
        {
            var result = _service.GetWithSpecializations();
            if (result == null || !result.Any())
                return NotFound("No departments found.");

            return Ok(result);
        }
    }
}
