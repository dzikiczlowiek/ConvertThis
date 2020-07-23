using System;

namespace ConvertThis.Infrastructure.Services.Converters
{
    public sealed class Base64ConverterSelector : IConverterSelector
    {
        public Type ConverterType => typeof(Base64Converter);

        public bool IsApplicable(string converterType)
        {
            return converterType.ToLowerInvariant() == "base64";
        }
    }
}
