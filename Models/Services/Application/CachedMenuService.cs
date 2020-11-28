using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreRecursiveMenuDemo.Models.Services.Infrastructure;
using Microsoft.Extensions.Caching.Memory;

namespace AspNetCoreRecursiveMenuDemo.Models.Services.Application
{
    public class CachedMenuService : IMenuService
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMemoryCache memoryCache;

        public CachedMenuService(IMenuRepository menuRepository, IMemoryCache memoryCache)
        {
            this.menuRepository = menuRepository;
            this.memoryCache = memoryCache;
        }

        public Task<List<MenuItem>> GetAllMenuItems()
        {
            return memoryCache.GetOrCreateAsync<List<MenuItem>>("menu", async cacheEntry => 
            {
                cacheEntry.SlidingExpiration = TimeSpan.FromHours(1);

                // First, we fetch all menu items from the underlying storage
                var menuItems = await menuRepository.GetAllMenuItems();

                // Then we group them by parentId
                var menuItemLookup = menuItems.ToLookup(menuItem => menuItem.ParentId, menuItem => menuItem);

                // Finally we assign children to each element
                foreach (var menuItem in menuItems)
                {
                    menuItem.Children = menuItemLookup[menuItem.Id].ToList();
                }
                
                // Return top level items only. All other menu items are in their Children collection.
                return menuItemLookup[(int?)null].ToList();
            });
        }
    }
}