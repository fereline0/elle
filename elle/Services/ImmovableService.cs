using System.Collections.Generic;
using System.Linq;
using elle.Models;
using Microsoft.EntityFrameworkCore;

namespace elle.Services
{
    public class ImmovableService
    {
        private readonly Context _context;

        public ImmovableService(Context context)
        {
            _context = context;
        }

        public List<Immovable> GetAll()
        {
            return _context.Immovables.ToList();
        }

        public bool DeleteById(int id)
        {
            var immovable = _context.Immovables.Find(id);

            if (immovable == null)
            {
                return false;
            }

            _context.Immovables.Remove(immovable);
            _context.SaveChangesAsync();
            return true;
        }
    }
}
