using System;
using System.Collections.Generic;

namespace Sips.SipsModels;

public partial class AddInOrderDetail
{
    public int AddInId { get; set; }

    public string OrderDetailId { get; set; }

    public int Quantity { get; set; }

    public virtual AddIn AddIn { get; set; } = null!;

    public virtual OrderDetail OrderDetail { get; set; } = null!;
    public virtual ICollection<AddInOrderDetail> AddInOrderDetails { get; set; } = new List<AddInOrderDetail>();
    public virtual Item Item { get; set; } = null!;
    public virtual ItemSize Size { get; set; } = null!;
    public virtual Transaction Transaction { get; set; } = null!;
}
