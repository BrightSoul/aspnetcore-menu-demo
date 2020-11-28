using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreRecursiveMenuDemo.Models.Services.Infrastructure
{
    public interface IMenuRepository
    {
        Task<List<MenuItem>> GetAllMenuItems();
    }
}