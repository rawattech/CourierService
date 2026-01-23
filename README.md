# Courier Service – Coding Challenge (.NET 8)

A command-line application built using .NET 8, Clean Architecture, SOLID principles, Dependency Injection, and Test-Driven Development (TDD).

# Features

* Delivery cost calculation with offer-based discounts
* Extensible offer strategy design
* Delivery time estimation using vehicle constraints
* Shipment selection based on business rules
* Fully unit-tested using mocks and DI

# appsettings.json

The application uses appsettings.json to store configuration values. This helps avoid hardcoding and makes changes easier.

 # What it contains

* Base delivery cost
* Vehicle details (count, max weight, speed)
* Offer details (code, weight range, distance range, discount)

# Tech Stack

* .NET 8
* C#
* xUnit, Moq, FluentAssertions
* Microsoft.Extensions.DependencyInjection

# Architecture

* Domain: Core business models
* Application: Interfaces and business rules
* Infrastructure: Concrete implementations
* Console App: Entry point

# Tests: Unit tests with mocked dependencies

- How to Run
- dotnet build
- dotnet test
- dotnet run --project CourierService.Console

# Notes

Designed for extensibility and scalability
Follows SOLID and clean code practices
Business logic is isolated and testable
