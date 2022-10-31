namespace D2W.WebPortal.Enums;

public enum InvoiceStatus
{
    Draft, // The invoice has been created, but it has not been sent to the client.
    Sent, // The invoice has been sent to the client.
    Viewed, // The invoice has been sent to the client.
    Partial, // The invoice has been partially paid.
    Paid, // The invoice has been paid in full.
    Overdue, // The invoice is past its due date with an amount due still outstanding.
    Canceled // The invoice has been manually marked as Canceled by the user.
}
