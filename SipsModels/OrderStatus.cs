using System;
using System.Collections.Generic;

namespace Sips.SipsModels;

public partial class OrderStatus
{
    public int StatusId { get; set; }

    public bool IsOrdered { get; set; }

    public bool IsPickedUp { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
