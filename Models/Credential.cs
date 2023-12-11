using System;
using System.Collections.Generic;

namespace Sips.Models;

public partial class Credential
{
    public int PkUserTypeId { get; set; }

    public int UserType { get; set; }

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}
