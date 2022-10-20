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
    public class ShoppingListRepos : Repository<ShoppingList>, IShoppingListRepos
    {
        private readonly ApplicationDbContext _context;
        public ShoppingListRepos(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ShoppingList shoppingList)
        {
            _context.ShoppingLists.Update(shoppingList);
        }
    }
}
