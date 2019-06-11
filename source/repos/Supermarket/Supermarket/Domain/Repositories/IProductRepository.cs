using System.Collections.Generic;
using System.Threading.Tasks;
using Supermarket.Domain.Models;

namespace Supermarket.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> ListAsync();
    }
}