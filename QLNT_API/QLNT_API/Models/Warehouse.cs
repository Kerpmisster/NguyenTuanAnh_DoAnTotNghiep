using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class Warehouse
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? Createat { get; set; }

    public byte? Status { get; set; }

    public virtual ICollection<ExportReceipt> ExportReceipts { get; set; } = new List<ExportReceipt>();

    public virtual ICollection<ImportReceipt> ImportReceipts { get; set; } = new List<ImportReceipt>();

    public virtual ICollection<WarehouseDetail> WarehouseDetails { get; set; } = new List<WarehouseDetail>();
}
