using System;

namespace ConvertThis.Infrastructure.Services
{
    [Serializable]
    public class MissingConverterException : Exception
    {
        public MissingConverterException(string converterType) : base($"Could not resolve '{converterType}' Converter")
        {
        }

        public MissingConverterException(string converterType, Exception innerException) : base($"Could not resolve '{converterType}' Converter", innerException)
        {
        }

        protected MissingConverterException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext) :base(serializationInfo,streamingContext)
        {
        }

        public static MissingConverterException Missing(string converterType) => new MissingConverterException(converterType);
    }
}
