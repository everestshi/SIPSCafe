using System;
using System.Collections.Generic;

namespace Sips.Models;

public partial class Rating
{
    public long PkRatingId { get; set; }

    public string Rating1 { get; set; } = null!;

    public byte[] Date { get; set; } = null!;

    public string Comment { get; set; } = null!;

    public long FkStoreId { get; set; }

    public long FkUserId { get; set; }

    public virtual Store FkStore { get; set; } = null!;

    public virtual Contact FkUser { get; set; } = null!;
}
