using System;
using System.Collections.Generic;

namespace Sips.Models;

public partial class ItemSize
{
    public int PkSizeId { get; set; }

    public string SizeName { get; set; } = null!;

    public decimal PriceModifier { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
