using System;
using System.Collections.Generic;

namespace Sips.SipsModels;

public partial class OrderDetail
{
    public string OrderDetailId { get; set; } = null!;

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public bool IsBirthdayDrink { get; set; }

    public decimal? PromoValue { get; set; }

    public int ItemId { get; set; }

    public string TransactionId { get; set; } = null!;

    public int SizeId { get; set; }

    public int? SweetnessId { get; set; }

    public int? IceId { get; set; }

    public virtual Ice? Ice { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual Sweetness? Sweetness { get; set; }
    public virtual ICollection<AddInOrderDetail> AddInOrderDetails { get; set; } = new List<AddInOrderDetail>();
}
