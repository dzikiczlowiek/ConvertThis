using ConvertThis.WebApi.Infrastructure;

namespace ConvertThis.Infrastructure.Services.Converters
{
    public sealed class Base64Converter : IConverter
    {
        public string Convert(byte[] input)
        {
            return System.Convert.ToBase64String(input);
        }
    }
}
