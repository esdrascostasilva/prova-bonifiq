using System;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using Xunit;

namespace ProvaPub.Tests;

public class CustomerServiceTests
{
    private TestDbContext GetDbContextWithData()
    {
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options; var context = new TestDbContext(options);

        context.Customers.Add(new Customer { Id = 1 });
        context.SaveChanges();
        return context;
    }

    [Fact]
    public async Task CanPurchase_FirstTimeCustomerUnderLimit_ReturnsTrue()
    {
        // Arrange 
        var context = GetDbContextWithData();
        var service = new CustomerService(context);

        // Act 
        var result = await
        service.CanPurchase(1, 50);

        // Assert 
        Assert.True(result);
    }

    [Fact]
    public async Task CanPurchase_FirstTimeCustomerOverLimit_ReturnsFalse()
    {
        var context = GetDbContextWithData();
        var service = new CustomerService(context);

        var result = await service.CanPurchase(1, 150);

        Assert.False(result);
    }
}
