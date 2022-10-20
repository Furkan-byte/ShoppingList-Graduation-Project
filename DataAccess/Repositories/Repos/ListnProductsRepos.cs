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
    public class ListnProductsRepos : Repository<ListnProducts>, IListnProductsRepos
    {
        private readonly ApplicationDbContext _context;
        public ListnProductsRepos(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ListnProducts listnProducts)
        {
            _context.ListnProducts.Update(listnProducts);
        }
    }
}
