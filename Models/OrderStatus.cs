using System;
using System.Collections.Generic;

namespace Sips.Models;

public partial class OrderStatus
{
    public long PkStatusId { get; set; }

    public string IsOrdered { get; set; } = null!;

    public string IsPickedUp { get; set; } = null!;

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
