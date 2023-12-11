using System;
using System.Collections.Generic;

namespace Sips.Models;

public partial class Rating
{
    public int PkRatingId { get; set; }

    public string Rating1 { get; set; } = null!;

    public DateTime Date { get; set; }

    public string Comment { get; set; } = null!;

    public int FkStoreId { get; set; }

    public int FkUserId { get; set; }

    public virtual Store FkStore { get; set; } = null!;

    public virtual Contact FkUser { get; set; } = null!;
}
