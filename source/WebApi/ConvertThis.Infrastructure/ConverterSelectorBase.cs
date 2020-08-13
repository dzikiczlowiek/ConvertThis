using System;

namespace ConvertThis.Infrastructure
{
    public abstract class ConverterSelectorBase<TConverter> : IConverterSelector
    {
        public Type ConverterType => typeof(TConverter);

        public abstract bool IsApplicable(string converterType);
    }
}
