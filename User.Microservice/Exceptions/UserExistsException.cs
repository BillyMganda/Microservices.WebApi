namespace User.Microservice.Exceptions
{
    public class UserExistsException : ApplicationException
    {
        public UserExistsException(string message) : base(message)
        {

        }
    }
}
