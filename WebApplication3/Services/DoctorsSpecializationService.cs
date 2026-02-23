using Microsoft.EntityFrameworkCore;
using ShopAPI.Models;

namespace ShopAPI.Services
{
    public class DoctorsSpecializationService
    {
        private readonly ShopDbContext _context;
        public DoctorsSpecializationService(ShopDbContext context)
        {
            _context = context;
        }
        public List<DoctorsSpecializations> GetAll() => _context.DoctorsSpecializations.ToList();
        public DoctorsSpecializations? GetById(int id) => _context.DoctorsSpecializations.FirstOrDefault(d => d.Id == id);
        public void Add(DoctorsSpecializations specialization)
        {
            _context.DoctorsSpecializations.Add(specialization);
            _context.SaveChanges();
        }
        public void Update(DoctorsSpecializations specialization)
        {
            _context.DoctorsSpecializations.Update(specialization);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var specialization = _context.DoctorsSpecializations.Find(id);
            if (specialization != null)
            {
                _context.DoctorsSpecializations.Remove(specialization);
                _context.SaveChanges();
            }
        }
        public List<DoctorsSpecializations> GetByDoctorId(int doctorId) =>
            _context.DoctorsSpecializations.Where(ds => ds.DoctorID == doctorId).ToList();
        public List<DoctorsSpecializations> GetBySpecializationId(int specializationId) =>
            _context.DoctorsSpecializations.Where(ds => ds.SpecializationID == specializationId).ToList();
    }
}