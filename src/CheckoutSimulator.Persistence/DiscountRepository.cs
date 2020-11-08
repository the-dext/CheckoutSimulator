// Checkout Simulator by Chris Dexter, file="DiscountRepository.cs"

namespace CheckoutSimulator.Persistence
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using CheckoutSimulator.Domain.Offers;
    using CheckoutSimulator.Domain.Repositories;

    /// <summary>
    /// Defines the <see cref="DiscountRepository"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DiscountRepository : IDiscountRepository
    {
        /// <summary>
        /// The GetDiscountsAsync.
        /// </summary>
        /// <returns>The <see cref="Task{IEnumerable{IDiscount}}"/>.</returns>
        public Task<IEnumerable<IDiscount>> GetDiscountsAsync()
        {
            var discounts = new List<IDiscount>()
            {
                new BuyOneGetOneFree("Buy One-Get One Free on Biscuits", "B15", 2, 0)
            };

            return Task.FromResult(discounts.AsEnumerable());
        }
    }
}
