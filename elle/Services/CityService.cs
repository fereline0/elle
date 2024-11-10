using System.Collections.Generic;
using System.Linq;
using elle.Models;
using Microsoft.EntityFrameworkCore;

namespace elle.Services
{
    public class CityService
    {
        private readonly Context _context;

        public CityService(Context context)
        {
            _context = context;
        }

        public List<City> GetAll()
        {
            return _context.Cities.ToList();
        }

        public bool DeleteById(int id)
        {
            var city = _context.Cities.Find(id);

            if (city == null)
            {
                return false;
            }

            _context.Cities.Remove(city);
            _context.SaveChanges();
            return true;
        }

        public void Add(City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();
        }

        public void Update(City city)
        {
            var existingCity = _context.Cities.Find(city.Id);
            if (existingCity != null)
            {
                existingCity.Name = city.Name;
                _context.SaveChanges();
            }
        }
    }
}
