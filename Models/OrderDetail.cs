using System;
using System.Collections.Generic;

namespace Sips.Models;

public partial class OrderDetail
{
    public long PkOrderDetailId { get; set; }

    public byte[] Price { get; set; } = null!;

    public long Quantity { get; set; }

    public string IsBirthdayDrink { get; set; } = null!;

    public byte[] PromoValue { get; set; } = null!;

    public long FkItemId { get; set; }

    public long FkTransactionId { get; set; }

    public long FkSizeId { get; set; }

    public virtual ICollection<AddInOrderDetail> AddInOrderDetails { get; set; } = new List<AddInOrderDetail>();

    public virtual Item FkItem { get; set; } = null!;

    public virtual ItemSize FkSize { get; set; } = null!;

    public virtual Transaction FkTransaction { get; set; } = null!;
}
