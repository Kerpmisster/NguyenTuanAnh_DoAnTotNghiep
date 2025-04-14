using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class EmailConfirmation
{
    public string Id { get; set; } = null!;

    public string? UserId { get; set; }

    public string? Code { get; set; }

    public DateTime? CreateAt { get; set; }

    public virtual AspNetUser? User { get; set; }
}
