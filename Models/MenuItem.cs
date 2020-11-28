using System.Collections.Generic;

namespace AspNetCoreRecursiveMenuDemo.Models
{
    public class MenuItem
    {
          public int Id { get; set; }
          public int? ParentId { get; set; }
          public string Href { get; set; }
          public string Text { get; set; }
          public List<string> Roles { get; set; } = new List<string>(); // Optional
          public List<MenuItem> Children { get; set; } = new List<MenuItem>();
    }
}