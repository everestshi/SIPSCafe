using System;
using System.Collections.Generic;

namespace Sips.SipsModels;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public bool IsBirthdayDrink { get; set; }

    public decimal? PromoValue { get; set; }

    public int ItemId { get; set; }

    public int? TransactionId { get; set; }

    public int SizeId { get; set; }
}
