using Microsoft.AspNetCore.Mvc;
using ShopAPI.DTOs;
using ShopAPI.Models;
using ShopAPI.Services;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorService _service;
        private readonly DepartmentsService _departmentsService;
        private readonly SpecializationService _specializationsService;

        public DoctorsController(DoctorService service, DepartmentsService departmentsService, SpecializationService specializationsService)
        {
            _service = service;
            _departmentsService = departmentsService;
            _specializationsService = specializationsService;
        }

        [HttpGet]
        public ActionResult<List<DoctorDto>> Get() =>
            Ok(_service.GetAll());

        [HttpGet("{id}")]
        public ActionResult<DoctorDto> Get(int id)
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

        [HttpGet("salary-above/{value}")]
        public ActionResult<List<DoctorDto>> GetBySalaryAbove(decimal value)
        {
            if (value <= 0)
                return BadRequest("Value must be greater than 0.");

            var doctors = _service.GetBySalaryAbove(value);
            if (doctors == null || !doctors.Any())
                return NotFound("No doctors found with salary above the specified value.");

            return Ok(doctors);
        }

        [HttpGet("by-department/{departmentId}")]
        public ActionResult<List<DoctorDto>> GetByDepartment(int departmentId)
        {
            if (departmentId <= 0)
                return BadRequest("DepartmentId must be greater than 0.");

            var department = _departmentsService.GetById(departmentId);
            if (department == null)
                return NotFound("Department not found.");

            var doctors = _service.GetByDepartment(departmentId);
            if (doctors == null || !doctors.Any())
                return NotFound("No doctors found in the specified department.");

            return Ok(doctors);
        }

        [HttpGet("by-specialization/{specializationId}")]
        public ActionResult<List<DoctorDto>> GetBySpecialization(int specializationId)
        {
            if (specializationId <= 0)
                return BadRequest("SpecializationId must be greater than 0.");

            var specialization = _specializationsService.GetById(specializationId);
            if (specialization == null)
                return NotFound("Specialization not found.");

            var doctors = _service.GetBySpecialization(specializationId);
            if (doctors == null || !doctors.Any())
                return NotFound("No doctors found with the specified specialization.");

            return Ok(doctors);
        }
    }
}
