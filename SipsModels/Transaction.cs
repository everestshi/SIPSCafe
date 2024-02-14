using System;
using System.Collections.Generic;

namespace Sips.SipsModels;

public partial class Transaction
{
    public string? TransactionId { get; set; }

    public DateTime DateOrdered { get; set; }

    public bool IsOrdered { get; set; }

    public bool IsPickedUp { get; set; }

    public int StoreId { get; set; }

    public int UserId { get; set; }

    public int? StatusId { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual OrderStatus? Status { get; set; }

    public virtual Store Store { get; set; } = null!;

    public virtual Contact User { get; set; } = null!;
}
