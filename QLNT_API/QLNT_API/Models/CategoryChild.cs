using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class CategoryChild
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }
}
