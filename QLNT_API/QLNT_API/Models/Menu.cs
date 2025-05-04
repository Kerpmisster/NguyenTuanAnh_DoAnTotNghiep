using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class Menu
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Slug { get; set; }

    public int? Parentid { get; set; }

    public int? Position { get; set; }

    public byte? Isactive { get; set; }

    public virtual ICollection<Menu> InverseParent { get; set; } = new List<Menu>();

    public virtual Menu? Parent { get; set; }
}
