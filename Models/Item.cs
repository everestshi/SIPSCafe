using System;
using System.Collections.Generic;

namespace Sips.Models;

public partial class Item
{
    public long PkItemId { get; set; }

    public string Name { get; set; } = null!;

    public byte[]? Image { get; set; }

    public string Description { get; set; } = null!;

    public string Ice { get; set; } = null!;

    public string Sweetness { get; set; } = null!;

    public byte[] BasePrice { get; set; } = null!;

    public long Inventory { get; set; }

    public string ItemType { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
