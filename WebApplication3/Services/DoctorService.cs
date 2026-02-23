using Microsoft.EntityFrameworkCore;
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

        public List<Doctors> GetAll() => _context.Doctors.ToList();
        public Doctors? GetById(int id) => _context.Doctors.FirstOrDefault(d => d.Id == id);
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
    }
}
