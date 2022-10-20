using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.InterfaceRepos
{
    public interface IUnitofWork
    {
        IApplicationUserRepos ApplicationUser { get; }
        ICategoryRepos Category { get; }
        IProductRepos Product { get; }
        IShoppingListRepos ShoppingList { get; }
        IListnProductsRepos ListnProducts { get; }
        void Save();
    }
}
