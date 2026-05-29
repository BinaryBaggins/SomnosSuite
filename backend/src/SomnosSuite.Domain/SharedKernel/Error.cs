namespace SomnosSuite.Domain.SharedKernel
{
    /** 
    * Represents an error with a code and a message.
    */
    public sealed record Error(string Code, string? Message)
    {
        /**
        * Represents the absence of an error.
        */
        public static readonly Error None = new("None", string.Empty);
    }
}

    