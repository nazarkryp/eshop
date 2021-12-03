namespace eShop.Web.Infrastructure.Errors
{
    public class ErrorResult
    {
        public ErrorResult(string name, string message)
        {
            Name = name;
            Message = message;
        }

        public string Name { get; set; }

        public string Message { get; set; }
    }
}
