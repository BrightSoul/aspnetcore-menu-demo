using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreRecursiveMenuDemo.Models.Services.Infrastructure
{
    public class FakeMenuRepository : IMenuRepository
    {
        public Task<List<MenuItem>> GetAllMenuItems()
        {
            return Task.FromResult(new List<MenuItem> {
                new MenuItem { Id = 1, ParentId = null, Text = "Home", Href = "/" },
                new MenuItem { Id = 2, ParentId = null, Text = "Products", Href = "/products/index" },
                new MenuItem { Id = 3, ParentId = 2, Text = "Plastic", Href = "/products/detail/plastic" },
                new MenuItem { Id = 4, ParentId = 2, Text = "Metal", Href = "/products/detail/metal" },
                new MenuItem { Id = 5, ParentId = 4, Text = "Adamantium", Href = "/products/detail/metal/adamantium" },
                new MenuItem { Id = 6, ParentId = 4, Text = "Vibranium", Href = "/products/detail/metal/vibranium" },
                new MenuItem { Id = 7, ParentId = 4, Text = "Organic steel", Href = "/products/detail/metal/organic-steel" },
                new MenuItem { Id = 8, ParentId = 2, Text = "Wood", Href = "/products/detail/wood" },
                new MenuItem { Id = 9, ParentId = 8, Text = "Oak", Href = "/products/detail/wood/oak" },
                new MenuItem { Id = 10, ParentId = 8, Text = "Pine", Href = "/products/detail/wood/pine" },
                new MenuItem { Id = 11, ParentId = null, Text = "Contact us", Href = "/contact-us" },
                new MenuItem { Id = 11, ParentId = null, Text = "Reserved area", Href = "/admin", Roles = { "Aministrator "} },
            });
        }
    }
}