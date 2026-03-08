using Microsoft.EntityFrameworkCore;
using ShopAPI.DTOs;
using ShopAPI.Models;

namespace ShopAPI.Services
{
    public class DoctorService
    {
        private readonly ShopDbContext _context;

        public DoctorService(ShopDbContext context)
        {
            _context = context;
        }

        private DoctorDto MapToDto(Doctors doctor)
        {
            var department = _context.Departments.FirstOrDefault(d => d.Id == doctor.DepartmentID);

            var specializations = (
                from ds in _context.DoctorsSpecializations
                join s in _context.Specializations on ds.SpecializationID equals s.Id
                where ds.DoctorID == doctor.Id
                select new SpecializationDto { Id = s.Id, Name = s.Name }
            ).ToList();

            return new DoctorDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Surname = doctor.Surname,
                Salary = doctor.Salary,
                Premium = doctor.Premium,
                DepartmentId = doctor.DepartmentID,
                DepartmentName = department?.Name ?? string.Empty,
                Specializations = specializations
            };
        }

        public List<DoctorDto> GetAll() =>
            _context.Doctors.ToList().Select(MapToDto).ToList();

        public DoctorDto? GetById(int id)
        {
            var doctor = _context.Doctors.FirstOrDefault(d => d.Id == id);
            return doctor == null ? null : MapToDto(doctor);
        }

        public void Add(Doctors doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }

        public void Update(Doctors doctor)
        {
            _context.Doctors.Update(doctor);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                _context.SaveChanges();
            }
        }

        public List<DoctorDto>? GetBySalaryAbove(decimal value)
        {
            var doctors = _context.Doctors.Where(d => d.Salary > value).ToList();
            if (!doctors.Any()) return null;
            return doctors.Select(MapToDto).ToList();
        }

        public List<DoctorDto>? GetByDepartment(int departmentId)
        {
            var doctors = _context.Doctors.Where(d => d.DepartmentID == departmentId).ToList();
            if (!doctors.Any()) return null;
            return doctors.Select(MapToDto).ToList();
        }

        public List<DoctorDto>? GetBySpecialization(int specializationId)
        {
            var doctorIds = _context.DoctorsSpecializations
                .Where(ds => ds.SpecializationID == specializationId)
                .Select(ds => ds.DoctorID)
                .ToList();

            if (!doctorIds.Any()) return null;

            var doctors = _context.Doctors.Where(d => doctorIds.Contains(d.Id)).ToList();
            if (!doctors.Any()) return null;

            return doctors.Select(MapToDto).ToList();
        }

        public List<DoctorDto>? GetAboveAverageInDepartment(int departmentId)
        {
            var doctors = _context.Doctors.Where(d => d.DepartmentID == departmentId).ToList();
            if (!doctors.Any()) return null;

            var avgSalary = doctors.Average(d => d.Salary);
            var aboveAvg = doctors.Where(d => d.Salary > avgSalary).ToList();

            return aboveAvg.Select(MapToDto).ToList();
        }
    }
}
