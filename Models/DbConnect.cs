using SinavOlusturma.Models.Entity;
using System.Data;
using System.Data.SQLite;

namespace SinavOlusturma.Models
{
    public class DbConnect
    {
        private SinavDbContext _context;

        public List<User> GetList()
        {
            List<User> dt = new List<User>();

            try
            {
                _context = new SinavDbContext();
                dt = _context.Users.ToList();
            }
            catch (Exception err)
            {
            }
            return dt;
        }
    }
}
