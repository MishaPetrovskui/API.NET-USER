using ShopAPI.DTOs;
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

        public List<Specializations> GetAll() =>
            _context.Specializations.ToList();

        public Specializations? GetById(int id) =>
            _context.Specializations.FirstOrDefault(s => s.Id == id);

        public void Add(Specializations item)
        {
            _context.Specializations.Add(item);
            _context.SaveChanges();
        }

        public void Update(Specializations item)
        {
            _context.Specializations.Update(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var item = _context.Specializations.Find(id);
            if (item != null)
            {
                _context.Specializations.Remove(item);
                _context.SaveChanges();
            }
        }

        public List<SpecializationWithCountDto>? GetWithDoctorCount()
        {
            var specs = _context.Specializations.ToList();
            if (!specs.Any()) return null;

            return specs.Select(s => new SpecializationWithCountDto
            {
                Id = s.Id,
                Name = s.Name,
                DoctorCount = _context.DoctorsSpecializations.Count(ds => ds.SpecializationID == s.Id)
            }).ToList();
        }
    }
}
