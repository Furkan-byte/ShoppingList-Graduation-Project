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
    public class ProductRepos : Repository<Product>, IProductRepos
    {
        private readonly ApplicationDbContext _context;
        public ProductRepos(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
