using System;
using System.Collections.Generic;

namespace Sips.Models;

public partial class Credential
{
    public long PkUserTypeId { get; set; }

    public long UserType { get; set; }

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}
