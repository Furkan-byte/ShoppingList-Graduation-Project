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
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _context;
        public UnitofWork(ApplicationDbContext context)
        {
            _context = context;
            ApplicationUser = new ApplicationUserRepos(_context);
            Category = new CategoryRepos(_context);
            Product = new ProductRepos(_context);
            ShoppingList = new ShoppingListRepos(_context);
            ListnProducts = new ListnProductsRepos(_context);
        }
        public IApplicationUserRepos ApplicationUser { get; private set; }

        public ICategoryRepos Category { get; private set; }

        public IProductRepos Product { get; private set; }

        public IShoppingListRepos ShoppingList { get; private set; }
        public IListnProductsRepos ListnProducts { get; set; }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
