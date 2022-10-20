using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.InterfaceRepos
{
    public interface IProductRepos : IRepository<Product>
    {
        void Update(Product product);

    }
}
