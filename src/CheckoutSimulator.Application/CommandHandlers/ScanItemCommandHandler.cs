// Checkout Simulator by Chris Dexter, file="ScanItemCommandHandler.cs"

namespace CheckoutSimulator.Application.CommandHandlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Ardalis.GuardClauses;
    using CheckoutSimulator.Application.Commands;
    using CheckoutSimulator.Domain;
    using MediatR;

    /// <summary>
    /// Defines the <see cref="ScanItemCommandHandler"/>.
    /// </summary>
    public class ScanItemCommandHandler : IRequestHandler<ScanItemCommand, bool>
    {
        private readonly ITill till;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanItemCommandHandler"/> class.
        /// </summary>
        /// <param name="till">The till <see cref="ITill"/>.</param>
        public ScanItemCommandHandler(ITill till)
        {
            this.till = Guard.Against.Null(till, nameof(till));
        }

        /// <summary>
        /// The Handle.
        /// </summary>
        /// <param name="cmd">The request <see cref="ScanItemCommand"/>.</param>
        /// <param name="cancellationToken">The cancellationToken <see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public Task<bool> Handle(ScanItemCommand cmd, CancellationToken cancellationToken)
        {
            _ = Guard.Against.Null(cmd, nameof(cmd));

            this.till.ScanItem(cmd.Barcode);

            return Task.FromResult(true);
        }
    }
}
