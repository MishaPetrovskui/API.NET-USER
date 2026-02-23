using Microsoft.EntityFrameworkCore;
using ShopAPI.Models;

namespace ShopAPI.Services
{
    public class SpecializationService
    {
        private readonly ShopDbContext _context;
        public SpecializationService(ShopDbContext context)
        {
            _context = context;
        }
        public List<Specializations> GetAll() => _context.Specializations.ToList();
        public DoctorsSpecializations? GetById(int id) => _context.DoctorsSpecializations.FirstOrDefault(d => d.Id == id);
        public void Add(Specializations specialization)
        {
            _context.Specializations.Add(specialization);
            _context.SaveChanges();
        }
        public void Update(Specializations specialization)
        {
            _context.Specializations.Update(specialization);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var specialization = _context.Specializations.Find(id);
            if (specialization != null)
            {
                _context.Specializations.Remove(specialization);
                _context.SaveChanges();
            }
        }
    }
}