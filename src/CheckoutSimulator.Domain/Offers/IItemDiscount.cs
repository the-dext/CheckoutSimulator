// Checkout Simulator by Chris Dexter, file="IRunningTotalDiscount.cs"

namespace CheckoutSimulator.Domain.Offers
{
    /// <summary>
    /// Defines the <see cref="IItemDiscount" />.
    /// This type of discount is applied as a till is scanning items
    /// </summary>
    public interface IItemDiscount : IDiscount
    {
    }
}
