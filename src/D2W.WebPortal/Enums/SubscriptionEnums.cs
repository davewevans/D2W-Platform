namespace D2W.WebPortal.Enums;

public enum SubscriptionStatus
{
    Active, // This means that we have received the payment of your subscription and it is now Active.
    PendingPayment, // This status indicates that the subscription has been created, but we are still waiting for the payment to hit our account.
    PendingCancellation, // When a subscription is manually canceled by the customer, its status is not usually transitioned to Cancelled immediately. This is because you have paid for a subscription until the end of the renewal date, so until the end of the pre-paid period, and you are entitled to use the subscription until then. When the pre-paid period ends, so right after the renewal is suppose to take place, the status of your subscription will change to Cancel.
    OnHold, // A subscription is placed On-Hold when an associated order is awaiting payment, or it has been manually suspended by the store owner or customer. A subscription can remain On-Hold indefinitely. If it was manually suspended, it will need to be manually reactivated. If it was suspended awaiting payment, it will be reactivated once that payment is processed.
    Canceled, // The Canceled status is assigned to subscriptions when they reach the end of their pre-paid term, so right after the renewal is suppose to take place but the customer has instead canceled his active subscription.
}
