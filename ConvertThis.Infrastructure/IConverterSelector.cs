using System;

namespace ConvertThis.Infrastructure
{
    public interface IConverterSelector
    {
        Type ConverterType { get; }

        bool IsApplicable(string converterType);
    }
}
