using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elle.Models;
using Microsoft.EntityFrameworkCore;

namespace elle.Services
{
    public class UserService
    {
        private readonly Context _context;

        public UserService(Context context)
        {
            _context = context;
        }

        public User FindUserByLogin(string login)
        {
            return _context.Users.SingleOrDefault(u => u.Login == login);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public bool DeleteById(int id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }
    }
}
