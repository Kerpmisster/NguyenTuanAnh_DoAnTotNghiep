using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class ImportReceipt
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public DateTime? Date { get; set; }

    public int? WarehouseId { get; set; }

    public int? SupplierId { get; set; }

    public decimal? Total { get; set; }

    public string? CreatedBy { get; set; }

    public virtual ICollection<ImportReceiptDetail> ImportReceiptDetails { get; set; } = new List<ImportReceiptDetail>();

    public virtual Supplier? Supplier { get; set; }

    public virtual Warehouse? Warehouse { get; set; }
}
