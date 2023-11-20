using courses_registration.Data;
using courses_registration.DTO;
using courses_registration.Interfaces;
using courses_registration.Models;
using System.Linq;

namespace courses_registration.Repositories
{
    public class LookupRepository : ILookupRepository
    {
        private readonly AppDbContext _context;
        public LookupRepository(AppDbContext context)
        {
            _context = context;  
        }
        public bool CreateLookup(Lookup lookup)
        {
            _context.Add(lookup);
            return Save();
        }

        public Lookup GetLookup(int id)
        {
            return _context.Lookups.Where(l => l.Id == id).FirstOrDefault();
        }

        public string GetLookupValue(string lookupName, int lookupId)
        {
            return _context.Lookups
                    .Where(l => l.LookupName == lookupName && l.LookupId == lookupId)
                    .Select(l => l.LookupValue)
                    .FirstOrDefault();
        }

        public List<Lookup> GetLookupValues(string lookupName)
        {
            return  _context.Lookups
                    .Where(l => l.LookupName == lookupName)
                    .ToList();
        }

        public bool LookupExists(int id)
        {
            return _context.Lookups.Any(l => l.Id == id) ;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Delete(Lookup lookup)
        {
            _context.Remove(lookup);
            return Save();
        }

        public bool UpdateLookup(Lookup lookup)
        {
            throw new NotImplementedException();
        }
    }
}
