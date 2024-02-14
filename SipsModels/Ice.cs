using System;
using System.Collections.Generic;

namespace Sips.SipsModels;

public partial class Ice
{

    public int IceId { get; set; }

    public decimal IcePercent { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
