using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNetCoreRecursiveMenuDemo.Models;
using AspNetCoreRecursiveMenuDemo.Models.Services.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCoreRecursiveMenuDemo.TagHelpers
{
    public class NavigationMenuTagHelper : TagHelper
    {
        private readonly IMenuService menuService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public NavigationMenuTagHelper(IMenuService menuService, IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.menuService = menuService;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var menuItems = await menuService.GetAllMenuItems();
            var userRoles = httpContextAccessor.HttpContext.User.FindAll(ClaimTypes.Role).Select(claim => claim.Value).ToList();
            RenderMenuItems(output, menuItems, userRoles);
        }

        private void RenderMenuItems(TagHelperOutput output, List<MenuItem> menuItems, List<string> userRoles)
        {
            if (menuItems.Count == 0)
            {
                return;
            }

            output.Content.AppendHtml("<ul>");
            foreach (var menuItem in menuItems)
            {
                if (menuItem.Roles.Count > 0 && menuItem.Roles.Intersect(userRoles).Count() == 0)
                {
                    // User does not have any of the necessary roles. Skip this menu Item.
                    continue;
                }

                output.Content.AppendHtml("<li>");
                output.Content.AppendHtml($"<a href=\"{menuItem.Href}\">{menuItem.Text}</a>");
                // Recurse over children
                RenderMenuItems(output, menuItem.Children, userRoles);
                output.Content.AppendHtml("</li>");
            }
            output.Content.AppendHtml("</ul>");
        }
    }
}