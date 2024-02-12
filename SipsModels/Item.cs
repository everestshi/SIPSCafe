using System;
using System.Collections.Generic;

namespace Sips.SipsModels;

public partial class Item
{
    public int ItemId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal BasePrice { get; set; }

    public int Inventory { get; set; }

    public string? UrlString { get; set; }

    public int? SweetnessId { get; set; }

    public int? IceId { get; set; }

    public int? ItemTypeId { get; set; }

    public virtual Ice? Ice { get; set; }

    public virtual ItemType? ItemType { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Sweetness? Sweetness { get; set; }
}
