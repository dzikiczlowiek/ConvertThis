using System.Text;

namespace ConvertThis.Infrastructure.Services
{
    public sealed class InputToByteArrayConverter : IInputToByteArrayConverter
    {
        public byte[] Convert(string input)
        {
            return UTF8Encoding.UTF8.GetBytes(input);
        }
    }
}
