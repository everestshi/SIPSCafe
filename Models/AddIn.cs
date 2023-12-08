using System;
using System.Collections.Generic;

namespace Sips.Models;

public partial class AddIn
{
    public long PkAddInId { get; set; }

    public string AddInName { get; set; } = null!;

    public byte[] PriceModifier { get; set; } = null!;

    public byte[]? Image { get; set; }

    public virtual ICollection<AddInOrderDetail> AddInOrderDetails { get; set; } = new List<AddInOrderDetail>();
}
