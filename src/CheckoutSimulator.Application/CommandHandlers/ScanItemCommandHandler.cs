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
        private readonly Till till;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScanItemCommandHandler"/> class.
        /// </summary>
        /// <param name="till">The till <see cref="Till"/>.</param>
        public ScanItemCommandHandler(Till till)
        {
            this.till = Guard.Against.Null(till, nameof(till));
        }

        /// <summary>
        /// The Handle.
        /// </summary>
        /// <param name="request">The request <see cref="ScanItemCommand"/>.</param>
        /// <param name="cancellationToken">The cancellationToken <see cref="CancellationToken"/>.</param>
        /// <returns>The <see cref="Task{bool}"/>.</returns>
        public Task<bool> Handle(ScanItemCommand request, CancellationToken cancellationToken)
        {
            _ = Guard.Against.Null(request, nameof(request));

            // do nothing
            return Task.FromResult(true);
        }
    }
}
