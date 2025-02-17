using System;
using System.Collections.Generic;

namespace Lab1.Models;

public partial class Loan
{
    public int LoanId { get; set; }

    public int? BookId { get; set; }

    public int? BorrowerId { get; set; }

    public DateOnly LoanDate { get; set; }

    public DateOnly ReturnDate { get; set; }

    public bool? Returned { get; set; }

    public virtual Book? Book { get; set; }

    public virtual Borrower? Borrower { get; set; }
}
