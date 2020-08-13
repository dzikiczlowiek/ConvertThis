namespace ConvertThis.Infrastructure
{
    public interface IInputToByteArrayConverter
    {
        byte[] Convert(string input);
    }
}
