#nullable enable

namespace _2c2p_test.Exceptions
{
    public class CannotFindServiceException : System.Exception
    {
        public CannotFindServiceException(string serviceName) : base($"Cannot find service {serviceName}") { }
    }
}