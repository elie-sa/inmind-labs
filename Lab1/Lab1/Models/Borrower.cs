using System;
using System.Collections.Generic;

namespace Lab1.Models;

public partial class Borrower
{
    public int BorrowerId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
