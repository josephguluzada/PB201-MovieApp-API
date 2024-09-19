namespace PB201MovieApp.MVC.UIExceptions.Common
{
    public class BadrequestException : Exception
    {
        public string PropertyName { get; set; }
        public BadrequestException()
        {
        }

        public BadrequestException(string? message) : base(message)
        {
        }

        public BadrequestException(string propertyName,string? message) : base(message)
        {
            PropertyName = propertyName;
        }

    }
}
