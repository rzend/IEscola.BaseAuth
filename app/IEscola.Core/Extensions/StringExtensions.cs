
namespace IEscola.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToCleanToken(this string token)
        {
            return token.Replace("Bearer ", "");
        }
    }
}
