namespace ManageYourBills.Domain.ValueObjects;
public record BillId
{
    public Guid Value { get; set; }

    public BillId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new EmptyBillIdException();
        }

        Value = value;
    }

    public static implicit operator Guid(BillId billId)
        => billId.Value;

    public static implicit operator BillId(Guid billId)
        => new(billId);
}
