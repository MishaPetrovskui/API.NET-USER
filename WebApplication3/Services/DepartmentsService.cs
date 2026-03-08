using ShopAPI.DTOs;
using ShopAPI.Models;

namespace ShopAPI.Services
{
    public class DepartmentsService
    {
        private readonly ShopDbContext _context;

        public DepartmentsService(ShopDbContext context)
        {
            _context = context;
        }

        public List<Departments> GetAll() => _context.Departments.ToList();

        public Departments? GetById(int id) =>
            _context.Departments.FirstOrDefault(d => d.Id == id);

        public void Add(Departments department)
        {
            _context.Departments.Add(department);
            _context.SaveChanges();
        }

        public void Update(Departments department)
        {
            _context.Departments.Update(department);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var department = _context.Departments.Find(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                _context.SaveChanges();
            }
        }

        public void DeleteAll()
        {
            var departments = _context.Departments.ToList();
            _context.Departments.RemoveRange(departments);
            _context.SaveChanges();
        }

        public List<DepartmentStatisticsDto>? GetStatistics()
        {
            var departments = _context.Departments.ToList();
            if (!departments.Any()) return null;

            var result = new List<DepartmentStatisticsDto>();

            foreach (var dept in departments)
            {
                var doctors = _context.Doctors.Where(d => d.DepartmentID == dept.Id).ToList();
                if (!doctors.Any()) continue;

                result.Add(new DepartmentStatisticsDto
                {
                    Id = dept.Id,
                    Name = dept.Name,
                    TotalExpenses = doctors.Sum(d => d.Salary + d.Premium),
                    AverageSalary = doctors.Average(d => d.Salary),
                    DoctorCount = doctors.Count
                });
            }

            return result.Any() ? result : null;
        }

        public List<DepartmentWithSpecializationsDto>? GetWithSpecializations()
        {
            var departments = _context.Departments.ToList();
            if (!departments.Any()) return null;

            return departments.Select(dept =>
            {
                var doctorIds = _context.Doctors
                    .Where(d => d.DepartmentID == dept.Id)
                    .Select(d => d.Id)
                    .ToList();

                var specializationNames = (
                    from ds in _context.DoctorsSpecializations
                    join s in _context.Specializations on ds.SpecializationID equals s.Id
                    where doctorIds.Contains(ds.DoctorID)
                    select s.Name
                ).Distinct().ToList();

                return new DepartmentWithSpecializationsDto
                {
                    Id = dept.Id,
                    Name = dept.Name,
                    Specializations = specializationNames
                };
            }).ToList();
        }
    }
}
