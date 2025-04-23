using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class ExportReceiptDetail
{
    public int Id { get; set; }

    public int? ExportReceiptId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual ExportReceipt? ExportReceipt { get; set; }
}
