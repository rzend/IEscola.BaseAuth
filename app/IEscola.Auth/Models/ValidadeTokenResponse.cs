namespace IEscola.Auth.Models
{
    public class ValidadeTokenResponse
    {
        public ValidadeTokenResponse(short statusCode)
        {
            StatusCode = statusCode;
        }

        public short StatusCode { get; set; }
    }
}
