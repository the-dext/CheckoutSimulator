// Checkout Simulator by Chris Dexter, file="AutofacModule.cs"

namespace CheckoutSimulator.Application.Setup
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Threading.Tasks;
    using Autofac;
    using CheckoutSimulator.Application.CommandHandlers;
    using CheckoutSimulator.Application.Commands;
    using CheckoutSimulator.Application.Queries;
    using CheckoutSimulator.Domain;
    using MediatR;
    using MediatR.Extensions.Autofac.DependencyInjection;

    /// <summary>
    /// Defines the <see cref="AutofacModule"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AutofacModule : Autofac.Module
    {
        /// <summary>
        /// The Load.
        /// </summary>
        /// <param name="builder">The builder <see cref="ContainerBuilder"/>.</param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();

            builder.RegisterType<GetStockItemsQueryHandler>().As<IRequestHandler<GetStockItemsQuery, IStockKeepingUnit[]>>();
            builder.RegisterMediatR(Assembly.GetExecutingAssembly());

            builder.Register<Till>((ctx) =>
            {
                return ctx.Resolve<TillFactory>()
                .CreateTillAsync()
                .Result;
            }).AsSelf().SingleInstance();

            base.Load(builder);
        }
    }
}
