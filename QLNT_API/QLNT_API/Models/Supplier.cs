using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public byte? Status { get; set; }

    public virtual ICollection<ImportReceipt> ImportReceipts { get; set; } = new List<ImportReceipt>();
}
