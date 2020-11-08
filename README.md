# CheckoutSimulator
A small demonstration design for an application that simulates a checkout process.

## Usage
Run CheckoutSimulator.Console to use the application interactively.
Prompts are on screen, just select one of the options.

[Q] will quit the application.

[V] will reset the till.

## Nuget packages used
* AutoFac - IoC container
* AutoFixture - A framework for creating anonymous data in unit tests.
* FluentAssertions - A assertion library to provide fluent syntax.
* GuardClauses - a library to make guard clause assertions a little bit quicker to write.
* MediatR - a mediation library to decouple assemblies.
* Moq - Mocking framework.
* xUnit - Unit test framework.


## Extensions used
Run Coverlet Report - An extension I wrote to display code coverage from Coverlet (https://marketplace.visualstudio.com/items?itemName=ChrisDexter.RunCoverletReport).

Create Unit Test Boiler Plate - An extension to quickly create the skeleton boiler plate code needed for unit testing a class.


### If there was more time...
If this was an actual project I would...

* Create behaviour tests using xBehave.net.
* Adjust the domain objects so that they were derived from either AggregateRoot, Entity, or Value types.
* I would also publish a domain event through Mediatr once the sale was complete for any interested parties to subscribe to (for example to adjust stock levels).
