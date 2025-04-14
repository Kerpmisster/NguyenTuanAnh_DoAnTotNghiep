using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class ProductMaterial
{
    public int Id { get; set; }

    public int? Pid { get; set; }

    public int? Mid { get; set; }

    public virtual Material? MidNavigation { get; set; }

    public virtual Product? PidNavigation { get; set; }
}
