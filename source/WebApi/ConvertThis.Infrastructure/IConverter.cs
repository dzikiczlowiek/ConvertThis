namespace ConvertThis.WebApi.Infrastructure
{
    public interface IConverter
    {
        string Convert(byte[] input);
    }

    public abstract class ConverterBase : IConverter
    {
        public string Convert(byte[] input)
        {
            throw new System.NotImplementedException();
        }
    }
}
