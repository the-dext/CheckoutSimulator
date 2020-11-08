// Checkout Simulator by Chris Dexter, file="Application.cs"

namespace CheckoutSimulator.Console
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading.Tasks;
    using Autofac;
    using CheckoutSimulator.Application.Commands;
    using CheckoutSimulator.Application.Queries;
    using CheckoutSimulator.Console.Setup;
    using MediatR;

    /// <summary>
    /// Defines the <see cref="Application" />.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Application
    {
        private static IContainer Container;

        private readonly IMediator AppMediator;

        private List<(string KeyboadShortCut, string Prompt, Func<Task<bool>> Function)> Commands;

        /// <summary>
        /// Initializes a new instance of the <see cref="Application"/> class.
        /// </summary>
        public Application()
        {
            var composition = new CompositionRoot();
            Container = composition.ConfigureIoc();
            this.AppMediator = Container.Resolve<IMediator>();
        }

        /// <summary>
        /// The Run.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task Run()
        {
            try
            {
                await this.SetupCommands();
                this.PrintAppBanner();
                this.PrintOptions();
                await this.WaitForCommand();
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                WriteLine("Command caused an exception: " + ex.Message, ConsoleColor.Red);
            }
        }

        /// <summary>
        /// The PrintAppBanner.
        /// </summary>
        private void PrintAppBanner()
        {
            WriteLine("******************************");
            WriteLine("***** Checkout Simulator *****");
            WriteLine("******************************");
            WriteLine();
        }

        /// <summary>
        /// The PrintOptions.
        /// </summary>
        private void PrintOptions()
        {
            foreach (var cmd in this.Commands)
            {
                if (cmd.KeyboadShortCut != string.Empty)
                {
                    WriteLine($"{cmd.KeyboadShortCut}: {cmd.Prompt}");
                }
                else
                {
                    WriteLine();
                }
            }
        }

        private static void WriteLine(string output = null, ConsoleColor colour = ConsoleColor.White)
        {
            if (output != null)
            {
                Console.ForegroundColor = colour;
                Console.WriteLine(output);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine();
            }
        }

        /// <summary>
        /// The SetupCommands.
        /// </summary>
        private async Task SetupCommands()
        {
            // General commands.
            this.Commands = new List<(string KeyBoardShortcut, string Prompt, Func<Task<bool>> Function)>()
            {
                ("Q", "Quit Application", () => { Environment.Exit(0); return Task.FromResult(true); }),
                ("T", "Request Scanning Total Price", async () =>
                {
                    var totalPrice = await this.AppMediator.Send(new GetTotalPriceQuery()).ConfigureAwait(true);

                    WriteLine($"*********", ConsoleColor.Green);
                    WriteLine($"Total price: {totalPrice:C2}", ConsoleColor.Green);
                    WriteLine($"*********", ConsoleColor.Green);

                    return true;
                }),
                (string.Empty, string.Empty, () => Task.FromResult(true)), // Null command to print a gap in the menu options.
            };


            // Commands per SKU
            var stockItems = (await this.AppMediator.Send(new GetStockItemsQuery())).OrderBy(x => x.Description);
            int keyCode = 1;
            foreach (var stockItem in stockItems)
            {
                this.Commands.Add(
                    (keyCode.ToString(),
                    stockItem.Description,
                    async () =>
                    {
                        var scanningResult = await this.AppMediator.Send(new ScanItemCommand(stockItem.Barcode));
                        if (scanningResult.Success)
                        {
                            WriteLine($"1 {stockItem.Description} @{stockItem.UnitPrice:C2}", ConsoleColor.Green);

                            if (!string.IsNullOrWhiteSpace(scanningResult.Message))
                            {
                                WriteLine($"*** {scanningResult.Message} ***", ConsoleColor.Yellow);
                            }

                            return true;
                        }
                        return false;
                    }
                ));
                keyCode++;
            }
        }

        /// <summary>
        /// The WaitForCommand.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task WaitForCommand()
        {
            while (true)
            {
                var isValidCommand = false;
                while (isValidCommand == false)
                {
                    var keyboardInput = Console.ReadLine();

                    isValidCommand = this.Commands.Any(c => c.KeyboadShortCut.Equals(keyboardInput, StringComparison.OrdinalIgnoreCase));

                    if (isValidCommand)
                    {
                        // find and run the command with the shortcut entered, we know it's there
                        // because valid is command is true.
                        var cmdResult = await this.Commands
                            .First(c => c.KeyboadShortCut.Equals(keyboardInput, StringComparison.OrdinalIgnoreCase))
                            .Function();
                    }
                }
            }
        }
    }
}
