namespace User.Microservice.Exceptions
{
    public class InvalidCredentialsException : ApplicationException
    {
        public InvalidCredentialsException(string message) : base(message)
        {

        }
    }
}
