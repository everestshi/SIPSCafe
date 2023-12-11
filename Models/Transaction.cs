using System;
using System.Collections.Generic;

namespace Sips.Models;

public partial class Transaction
{
    public int PkTransactionId { get; set; }

    public DateTime DateOrdered { get; set; }

    public int FkStoreId { get; set; }

    public int FkUserId { get; set; }

    public int FkStatusId { get; set; }

    public virtual OrderStatus FkStatus { get; set; } = null!;

    public virtual Store FkStore { get; set; } = null!;

    public virtual Contact FkUser { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
