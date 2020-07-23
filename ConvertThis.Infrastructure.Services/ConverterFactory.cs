using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ConvertThis.WebApi.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

namespace ConvertThis.Infrastructure.Services
{
    public class ConverterFactory : IConverterFactory, IDisposable
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
                return new NullConverter(converterType);
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

        public void Dispose()
        {
            // TODO;
        }

        internal sealed class NullConverter : IConverter
        {
            private readonly string _converterName;

            public NullConverter(string converterName)
            {
                _converterName = converterName;
            }

            public string Convert(byte[] input)
            {
                return $"Missing {_converterName} converter.";
            }
        }
    }


    public sealed class InputToByteArrayConverter : IInputToByteArrayConverter
    {
        public byte[] Convert(string input)
        {
            return UTF8Encoding.UTF8.GetBytes(input);
        }
    }
}
