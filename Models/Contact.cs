using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sips.Models;

public partial class Contact
{
    [Display(Name = "Customer Id")]

    public int PkUserId { get; set; }
    [Display(Name = "First Name")]

    public string FirstName { get; set; } = null!;
    [Display(Name = "Last Name")]

    public string? LastName { get; set; }
    [Display(Name = "Phone Number")]

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Unit { get; set; }

    public string Street { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Province { get; set; } = null!;
    [Display(Name = "Postal Code")]

    public string PostalCode { get; set; } = null!;
    [Display(Name = "Birth Date")]
    [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = false)]
    public DateTime? BirthDate { get; set; }

    public string IsDrinkRedeemed { get; set; } = null!;

    public int FkUserTypeId { get; set; }

    public virtual Credential? FkUserType { get; set; } = null!;

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
