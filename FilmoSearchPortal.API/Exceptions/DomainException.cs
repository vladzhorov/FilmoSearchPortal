namespace FilmoSearchPortal.API.Exceptions
{
    [Serializable]
    public abstract class DomainException : Exception
    {
        public HttpStatusCode HttpStatusCode { get; }

        protected DomainException(string message, HttpStatusCode statusCode) : base(message)
        {
            HttpStatusCode = statusCode;
        }

        protected DomainException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public abstract string Code { get; }
    }
}