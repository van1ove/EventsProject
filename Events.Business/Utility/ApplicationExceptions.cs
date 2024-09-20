namespace Events.Business.Utility
{
    public class InvalidPasswordException : ApplicationException
    {
        public InvalidPasswordException(string? message) : base(message) { }
    }

    public class InvalidTokenException : ApplicationException
    {
        public InvalidTokenException(string? message) : base(message) { }
    }

    public class DuplicatedObjectException : ApplicationException 
    {
        public DuplicatedObjectException(string? message) : base(message) { }
    }
}
