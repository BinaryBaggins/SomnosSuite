using SomnosSuite.Domain.SharedKernel;

namespace SomnosSuite.Domain.Animals
{
    public sealed record Animal : IValueObject
    {
        public AnimalKind Kind { get; }
        public AnimalCategory Category => AnimalClassification.GetCategory(Kind);
        public string? EarTagNumber { get; }
        public string? SupplierName { get; }

        private Animal() { }

        private Animal(
        AnimalKind kind, string? earTagNumber,
        string? supplierName)
        {
            Kind = kind;
            EarTagNumber = earTagNumber;
            SupplierName = supplierName;
        }

        public static Result<Animal> Create(AnimalKind kind, string? earTagNumber, string? supplierName)
        {

            if (!Enum.IsDefined(kind))
                return Result<Animal>.Failure(AnimalErrors.AnimalKindIsInvalidError);

            var category = AnimalClassification.GetCategory(kind);

            var trimmedEarTagNumber = NormalizeOptionalText(earTagNumber);
            var trimmedSupplierName = NormalizeOptionalText(supplierName);

            if (category == AnimalCategory.Grossvieh && string.IsNullOrWhiteSpace(trimmedEarTagNumber))
                return Result<Animal>.Failure(AnimalErrors.EarTagNumberIsRequiredForGrossviehError);

            if (category == AnimalCategory.Grossvieh && string.IsNullOrWhiteSpace(trimmedSupplierName))
                return Result<Animal>.Failure(AnimalErrors.SupplierNameIsRequiredForGrossviehError);

            if (kind == AnimalKind.Kalb && string.IsNullOrWhiteSpace(trimmedEarTagNumber))
                return Result<Animal>.Failure(AnimalErrors.EarTagNumberIsRequiredForKalbError);

            if (kind == AnimalKind.Kalb && string.IsNullOrWhiteSpace(trimmedSupplierName))
                return Result<Animal>.Failure(AnimalErrors.SupplierNameIsRequiredForKalbError);

            return new Animal(kind, trimmedEarTagNumber, trimmedSupplierName);
        }

        private static string? NormalizeOptionalText(string? value)
        {
            var trimmedValue = value?.Trim();
            return string.IsNullOrWhiteSpace(trimmedValue) ? null : trimmedValue;
        }
    }
}
