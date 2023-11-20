using courses_registration.DTO;
using courses_registration.Models;

namespace courses_registration.Interfaces
{
    public interface ILookupRepository
    {
        Lookup GetLookup(int id);
        bool LookupExists(int id);
        bool CreateLookup(Lookup lookup);
        bool UpdateLookup(Lookup lookup);
        string GetLookupValue(string lookupName,int lookupId);

        List<Lookup> GetLookupValues(string lookupName);
        bool Delete(Lookup lookup);
        bool Save();
    }
}
