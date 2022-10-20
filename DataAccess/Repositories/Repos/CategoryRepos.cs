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
    public class CategoryRepos : Repository<Category>, ICategoryRepos
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepos(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
