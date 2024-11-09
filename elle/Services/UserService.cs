using System.Collections.Generic;
using System.Linq;
using elle.Models;

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

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            var existingUser = _context.Users.Find(user.Id);
            if (existingUser != null)
            {
                existingUser.Login = user.Login;
                existingUser.Password = user.Password;
                existingUser.Role = user.Role;
                _context.SaveChanges();
            }
        }
    }
}
