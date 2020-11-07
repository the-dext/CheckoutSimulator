// Checkout Simulator by Chris Dexter, file="Till.cs"

namespace CheckoutSimulator.Domain
{
    using System;

    /// <summary>
    /// Defines the <see cref="Till"/>.
    /// </summary>
    public class Till
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Till"/> class.
        /// </summary>
        public Till()
        {
            this.ObjectId = Guid.NewGuid();
            System.Diagnostics.Debug.WriteLine($"Till created: {this.ObjectId}");
        }

        /// <summary>
        /// Gets the object identifier. Useful for debugging to check object references are different/equal..
        /// </summary>
        public Guid ObjectId { get; }
    }
}
