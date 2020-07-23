using System;

namespace ConvertThis.Infrastructure.Services.Converters
{
    public sealed class Base32ConverterSelector : IConverterSelector
    {
        public Type ConverterType => typeof(Base32Converter);

        public bool IsApplicable(string converterType)
        {
            return converterType.ToLowerInvariant() == "base32";
        }
    }
}
