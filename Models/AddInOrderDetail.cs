using System;
using System.Collections.Generic;

namespace Sips.Models;

public partial class AddInOrderDetail
{
    public long FkAddInId { get; set; }

    public long FkOrderDetailId { get; set; }

    public long Quantity { get; set; }

    public virtual AddIn FkAddIn { get; set; } = null!;

    public virtual OrderDetail FkOrderDetail { get; set; } = null!;
}
