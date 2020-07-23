namespace ConvertThis.WebApi.Infrastructure
{
    public interface IConverter
    {
        string Convert(byte[] input);
    }
}
