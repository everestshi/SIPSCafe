using System;
using System.Collections.Generic;

namespace Sips.SipsModels;

public partial class OrderDetail
{
    public string OrderDetailId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public bool IsBirthdayDrink { get; set; }

    public decimal? PromoValue { get; set; }

    public int ItemId { get; set; }

    public string? TransactionId { get; set; }

    public int SizeId { get; set; }
    public virtual ICollection<AddInOrderDetail> AddInOrderDetails { get; set; } = new List<AddInOrderDetail>();
    public virtual Item Item { get; set; } = null!;
    public virtual ItemSize Size { get; set; } = null!;
    public virtual Transaction Transaction { get; set; } = null!;
}
