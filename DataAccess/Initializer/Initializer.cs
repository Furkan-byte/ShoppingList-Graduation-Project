using DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Initializer
{
    public class Initializer : InterfaceInit
    {
        private readonly ApplicationDbContext _context;

        public Initializer(
            ApplicationDbContext context)
        {
            _context = context;

        }
        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }


    }
}
