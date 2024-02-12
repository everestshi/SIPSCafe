using System;
using System.Collections.Generic;

namespace Sips.SipsModels;

public partial class Sweetness
{
    public int SweetnessId { get; set; }

    public decimal SweetnessPercent { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
