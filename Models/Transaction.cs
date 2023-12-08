using System;
using System.Collections.Generic;

namespace Sips.Models;

public partial class Transaction
{
    public long PkTransactionId { get; set; }

    public byte[] DateOrdered { get; set; } = null!;

    public long FkStoreId { get; set; }

    public long FkUserId { get; set; }

    public long FkStatusId { get; set; }

    public virtual OrderStatus FkStatus { get; set; } = null!;

    public virtual Store FkStore { get; set; } = null!;

    public virtual Contact FkUser { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
