using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageYourBills.Domain.Entities;
public class Bill
{
    public BillId Id { get; private set; }
    public BillType Type { get; set; }
    public Provider Provider { get; set; }
    public DateOnly Issued { get; private set; }
    public DateOnly From { get; private set; }
    public DateOnly To { get; private set; }
    public DateOnly Expiration { get; private set; }
    public string? Comments { get; set; }
    public decimal Cost { get; set; }
    public bool IsPaid { get; set; }
    public DateOnly? PaidAt { get; private set; }

    public Bill(Guid id, BillType type, Provider provider, DateOnly issued, DateOnly from, DateOnly to, DateOnly expiration,
        string comments, decimal cost, bool isPaid, DateOnly? paidAt)
    {

        Id = id;
        Type = type;

        if (provider is null)
        {
            throw new NullProviderException();
        }

        Provider = provider;

        if (from > to)
        {
            throw new InvalidDateException("Date From can not be larger than Date To");
        }

        From = from;
        To = to;

        if (expiration < to)
        {
            throw new InvalidDateException("Date To can not be larger than Date Expiration");
        }

        Expiration = expiration;

        if (issued < from)
        {
            throw new InvalidDateException("Issue date can not be smaller than the start consumption date");
        }

        Issued = issued;

        if (comments is not null && comments.Length > 500)
        {
            throw new TooLongCommentsException();
        }

        Comments = comments;
        Cost = cost;
        IsPaid = isPaid;

        if (IsPaid && paidAt is null)
        {
            throw new InvalidDateException("If IsPaid is true PaidAt can not be null");
        }

        PaidAt = paidAt;
    }

    public void ChangeDates(DateOnly issued, DateOnly from, DateOnly to, DateOnly expiration, DateOnly? paidAt)
    {
        if (from > to)
        {
            throw new InvalidDateException("Date From can not be larger than Date To");
        }

        From = from;
        To = to;

        if (expiration < to)
        {
            throw new InvalidDateException("Date To can not be larger than Date Expiration");
        }

        Expiration = expiration;

        if (issued < from)
        {
            throw new InvalidDateException("Issue date can not be smaller than the start consumption date");
        }

        Issued = issued;


        if (IsPaid && paidAt is null)
        {
            throw new InvalidDateException("If IsPaid is true PaidAt can not be null");
        }

        PaidAt = paidAt;
    }

    private Bill()
    {
    }
}
