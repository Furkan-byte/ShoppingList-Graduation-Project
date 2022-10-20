using DataAccess.Database;
using DataAccess.Repositories.InterfaceRepos;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Repos
{
    public class ApplicationUserRepos : Repository<ApplicationUser>,IApplicationUserRepos
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRepos(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
