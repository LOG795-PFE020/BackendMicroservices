﻿using Bogus;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Stock.Commands.Interfaces;
using StockServiceIntegrationTests.Infrastructure;
using StockServiceIntegrationTests.Tests.Stocks.Services;

namespace StockServiceIntegrationTests.Tests.Stocks;

[Collection(nameof(TestCollections.Default))]
public sealed class QuoteConsumerTests
{
    private readonly ApplicationFactoryFixture _applicationFactoryFixture;

    public QuoteConsumerTests(ApplicationFactoryFixture applicationFactoryFixture)
    {
        _applicationFactoryFixture = applicationFactoryFixture;
    }

    [Fact]
    public async Task WithValidStockQuote_QuoteConsumer_ShouldIndexShareSuccessfully()
    {
        var faker = new Faker();
        var quote = await QuotePublisher.PublishQuote(_applicationFactoryFixture, faker.Company.CompanyName());

        using var scope = _applicationFactoryFixture.Services.CreateScope();

        ISharesRepository sharesRepository = scope.ServiceProvider.GetRequiredService<ISharesRepository>();

        var share = await sharesRepository.GetBySymbolAsync(quote.Symbol);

        share.Should().NotBeNull();

        share.GetPrice(quote.Date.Date).Content.Should().Be(100);
    }
}