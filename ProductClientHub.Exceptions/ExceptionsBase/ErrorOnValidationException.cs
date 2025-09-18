namespace ProductClientHub.Exceptions.ExceptionsBase
{
    public class ErrorOnValidationException : ProductClientHubException
    {

        private readonly List<string> _errors;
        public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
        {
            _errors = errorMessages;
        }

        public override List<string> GetErrors()
        {
            return _errors;
        }

        public override System.Net.HttpStatusCode GetHttpStatusCode()
        {
            return System.Net.HttpStatusCode.BadRequest;
        }
    }
}
