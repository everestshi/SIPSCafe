using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Sips.Models;

public partial class AddIn
{
    public int PkAddInId { get; set; }

    public string AddInName { get; set; } = null!;

    public decimal PriceModifier { get; set; }

    public string urlString { get; set; } = null!;

    public virtual ICollection<AddInOrderDetail> AddInOrderDetails { get; set; } = new List<AddInOrderDetail>();
}
