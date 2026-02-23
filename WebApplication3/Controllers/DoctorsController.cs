using Microsoft.AspNetCore.Mvc;
using ShopAPI.Models;
using ShopAPI.Services;

namespace ShopAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorService _doctorService;

        public DoctorsController(DoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public ActionResult<List<Doctors>> GetDoctors()
        {
            return Ok(_doctorService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Doctors> GetDoctor(int id)
        {
            var order = _doctorService.GetById(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        public ActionResult CreateDoctor(Doctors doctor)
        {
            _doctorService.Add(doctor);
            return Ok();
        }
        [HttpPost]
        public ActionResult UpdateDoctor(Doctors doctor)
        {
            _doctorService.Update(doctor);
            return Ok();
        }
        [HttpPost("{id}")]
        public ActionResult DeleteDoctor(int id)
        {
            _doctorService.Delete(id);
            return Ok();
        }
    }
}
