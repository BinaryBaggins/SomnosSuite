namespace SomnosSuite.Domain.SharedKernel;

/// <summary>
/// Represents the outcome of an operation that returns a value on success.
/// </summary>
public sealed class Result<T> : Result
{
    private readonly T? _value;

    public T Value =>
        IsSuccess
            ? _value!
            : throw new InvalidOperationException(
                "Cannot access the value of a failed result.");

    private Result(T value) : base(true, Error.None)
    {
        _value = value;
    }

    private Result(Error error) : base(false, error)
    {
        _value = default;
    }

    public static Result<T> Success(T value) => new(value);

    public new static Result<T> Failure(Error error) => new(error);

    public static implicit operator Result<T>(T value) => Success(value);
}