using System;
using System.Collections.Generic;
using System.Linq;

using ConvertThis.WebApi.Infrastructure;

namespace ConvertThis.Infrastructure.Services
{
    public sealed class ConverterFactory : IConverterFactory
    {
        private readonly IServiceProvider _serviceScope;
        private readonly IEnumerable<IConverterSelector> _converterSelectors;

        public ConverterFactory(IServiceProvider serviceScope, IEnumerable<IConverterSelector> converterSelectors)
        {
            _serviceScope = serviceScope;
            _converterSelectors = converterSelectors;
        }

        public IConverter Create(string converterType)
        {
            var selector = _converterSelectors.SingleOrDefault(x => x.IsApplicable(converterType));
            if(selector == null)
            {
                throw MissingConverterException.Missing(converterType);
            }

            return (IConverter)_serviceScope.GetService(selector.ConverterType);
        }

        public void Release(IConverter converter)
        {
            if(converter is IDisposable)
            {
                ((IDisposable)converter).Dispose();
            }
        }
    }
}
