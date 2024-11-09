using System.Collections.Generic;
using System.Linq;
using elle.Models;

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
            _context.SaveChanges();
            return true;
        }

        public void Add(Immovable immovable)
        {
            _context.Immovables.Add(immovable);
            _context.SaveChanges();
        }

        public void Update(Immovable immovable)
        {
            var existingImmovable = _context.Immovables.Find(immovable.Id);
            if (existingImmovable != null)
            {
                existingImmovable.Name = immovable.Name;
                existingImmovable.Address = immovable.Address;
                existingImmovable.Price = immovable.Price;
                _context.SaveChanges();
            }
        }
    }
}
