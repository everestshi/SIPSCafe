using System;
using System.Collections.Generic;

namespace Sips.Models;

public partial class OrderDetail
{
    public int PkOrderDetailId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public string IsBirthdayDrink { get; set; } = null!;

    public decimal PromoValue { get; set; }

    public int FkItemId { get; set; }

    public int FkTransactionId { get; set; }

    public int FkSizeId { get; set; }

    public virtual ICollection<AddInOrderDetail> AddInOrderDetails { get; set; } = new List<AddInOrderDetail>();

    public virtual Item FkItem { get; set; } = null!;

    public virtual ItemSize FkSize { get; set; } = null!;

    public virtual Transaction FkTransaction { get; set; } = null!;
}
