using System;
using System.Collections.Generic;

namespace Sips.Models;

public partial class Contact
{
    public long PkUserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long Unit { get; set; }

    public string Street { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Province { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public byte[]? BirthDate { get; set; }

    public string IsDrinkRedeemed { get; set; } = null!;

    public long FkUserTypeId { get; set; }

    public virtual Credential FkUserType { get; set; } = null!;

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
