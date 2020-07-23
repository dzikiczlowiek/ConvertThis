using ConvertThis.WebApi.Infrastructure;

namespace ConvertThis.Infrastructure
{
    public interface IConverterFactory
    {
        IConverter Create(string converterType);

        void Release(IConverter converter);
    }
}
