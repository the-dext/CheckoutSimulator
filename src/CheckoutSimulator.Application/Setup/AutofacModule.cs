// Checkout Simulator by Chris Dexter, file="AutofacModule.cs"

namespace CheckoutSimulator.Application.Setup
{
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using Autofac;
    using CheckoutSimulator.Application.CommandHandlers;
    using CheckoutSimulator.Application.Commands;
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
            builder.RegisterMediatR(Assembly.GetExecutingAssembly());
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsImplementedInterfaces();

            builder.RegisterType<Till>().AsSelf().SingleInstance();
            base.Load(builder);
        }
    }
}
