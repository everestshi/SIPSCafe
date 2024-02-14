using System;
using System.Collections.Generic;

namespace Sips.SipsModels;

public partial class Ice
{

    public int IceId { get; set; }

    public decimal IcePercent { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
