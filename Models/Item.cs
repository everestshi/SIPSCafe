using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Sips.Models;

public partial class Item
{
    public int PkItemId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Ice { get; set; } = null!;

    public string Sweetness { get; set; } = null!;

    public decimal BasePrice { get; set; }

    public int Inventory { get; set; }

    public string ItemType { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
