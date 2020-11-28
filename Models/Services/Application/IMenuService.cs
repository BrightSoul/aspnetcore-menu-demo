using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreRecursiveMenuDemo.Models.Services.Application
{
    public interface IMenuService
    {
        Task<List<MenuItem>> GetAllMenuItems();
    }
}