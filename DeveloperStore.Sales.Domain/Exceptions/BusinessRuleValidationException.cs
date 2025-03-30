namespace DeveloperStore.Sales.Domain.Exceptions
{
    public class BusinessRuleValidationException : Exception
    {
        public BusinessRuleValidationException(string message) : base(message) { }
        public BusinessRuleValidationException(string message, Exception? innerException) : base(message, innerException) { }
    }
}
