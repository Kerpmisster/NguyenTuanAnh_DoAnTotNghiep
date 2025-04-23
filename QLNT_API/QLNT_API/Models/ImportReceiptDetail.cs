using System;
using System.Collections.Generic;

namespace QLNT_API.Models;

public partial class ImportReceiptDetail
{
    public int Id { get; set; }

    public int? ImportReceiptId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual ImportReceipt? ImportReceipt { get; set; }

    public virtual Product? Product { get; set; }
}
