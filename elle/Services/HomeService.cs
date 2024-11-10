using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elle.Models;
using Microsoft.EntityFrameworkCore;

namespace elle.Services
{
    public class HomeService
    {
        private readonly Context _context;

        public HomeService(Context context)
        {
            _context = context;
        }

        public List<Home> GetAll()
        {
            return _context.Homes.ToList();
        }

        public bool DeleteById(int id)
        {
            var home = _context.Homes.Find(id);

            if (home == null)
            {
                return false;
            }

            _context.Homes.Remove(home);
            _context.SaveChanges();
            return true;
        }

        public void Add(Home home)
        {
            _context.Homes.Add(home);
            _context.SaveChanges();
        }

        public void Update(Home home)
        {
            var existingHome = _context.Homes.Find(home.Id);
            if (existingHome != null)
            {
                existingHome.Name = home.Name;
                existingHome.CityId = home.CityId;
                _context.SaveChanges();
            }
        }
    }
}
