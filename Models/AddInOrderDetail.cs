using System;
using System.Collections.Generic;

namespace Sips.Models;

public partial class AddInOrderDetail
{
    public int FkAddInId { get; set; }

    public int FkOrderDetailId { get; set; }

    public int Quantity { get; set; }

    public virtual AddIn FkAddIn { get; set; } = null!;

    public virtual OrderDetail FkOrderDetail { get; set; } = null!;
}
