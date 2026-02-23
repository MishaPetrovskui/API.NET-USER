using Microsoft.EntityFrameworkCore;
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
        public Departments? GetById(int id) => _context.Departments.FirstOrDefault(d => d.Id == id);
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
    }
}
