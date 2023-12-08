using System;
using System.Collections.Generic;

namespace Sips.Models;

public partial class ItemSize
{
    public long PkSizeId { get; set; }

    public string SizeName { get; set; } = null!;

    public byte[] PriceModifier { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
