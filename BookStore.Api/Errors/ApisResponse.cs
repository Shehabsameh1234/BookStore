namespace BookStore.Api.Errors
{
    public class ApisResponse
    {
        public int StatusCode { get; set; }
        public string? Messege { get; set; }
        public ApisResponse(int statusCode,string? messege =null)
        {
            StatusCode = statusCode;
            Messege = messege ?? GetDefaultMessege(statusCode);
        }
        public string? GetDefaultMessege(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                404 => "Not Found",
                401 => "Not Authorized",
                500 => "error",
                _ => null
            };

        }
    }
}
