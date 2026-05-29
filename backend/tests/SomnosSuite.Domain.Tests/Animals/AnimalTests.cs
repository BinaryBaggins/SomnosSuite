using FluentAssertions;
using SomnosSuite.Domain.Animals;
using Xunit;

namespace SomnosSuite.Domain.Tests.Animals;

public sealed class AnimalTests
{
    [Theory]
    [InlineData(AnimalKind.Rind)]
    [InlineData(AnimalKind.Kuh)]
    [InlineData(AnimalKind.Muni)]
    [InlineData(AnimalKind.Ochse)]
    public void Create_Should_Require_EarTag_And_Supplier_For_Grossvieh(AnimalKind kind)
    {
        Animal.Create(kind, null, "supplier").Error
            .Should().Be(AnimalErrors.EarTagNumberIsRequiredForGrossviehError);

        Animal.Create(kind, "ear-tag", " ").Error
            .Should().Be(AnimalErrors.SupplierNameIsRequiredForGrossviehError);
    }

    [Fact]
    public void Create_Should_Require_EarTag_And_Supplier_For_Kalb()
    {
        Animal.Create(AnimalKind.Kalb, null, "supplier").Error
            .Should().Be(AnimalErrors.EarTagNumberIsRequiredForKalbError);

        Animal.Create(AnimalKind.Kalb, "ear-tag", " ").Error
            .Should().Be(AnimalErrors.SupplierNameIsRequiredForKalbError);
    }

    [Fact]
    public void Create_Should_Normalize_Blank_Optional_Text_To_Null()
    {
        var result = Animal.Create(AnimalKind.Schwein, " ", "");

        result.IsSuccess.Should().BeTrue();
        result.Value.EarTagNumber.Should().BeNull();
        result.Value.SupplierName.Should().BeNull();
    }

    [Fact]
    public void Create_Should_Trim_Valid_Text()
    {
        var result = Animal.Create(AnimalKind.Kalb, "  CH-123  ", "  Farm AG  ");

        result.IsSuccess.Should().BeTrue();
        result.Value.EarTagNumber.Should().Be("CH-123");
        result.Value.SupplierName.Should().Be("Farm AG");
    }
}
