using System.Linq;
using System.Text;

namespace Camellia
{
    public static class Utils
    {
        public static string ToHexdump(byte[] data, int width = 16)
        {
            var builder = new StringBuilder();

            for (int i = 0; i < data.Length; i += width)
            {
                var slice = data.Skip(i).Take(width).ToArray();

                // Hex dump
                builder.Append(string.Join(" ", slice.Select(x => x.ToString("X2"))).PadRight((width * 3) + 1));

                // ASCII dump
                builder.Append(string.Join("", slice.Select(ToPrintable)));

                builder.AppendLine();
            }

            return builder.ToString();
        }

        public static string ToPrintable(byte octet)
        {
            // Whether the character is not printable
            if (octet < ' ' || octet > '~') return ".";
            return ((char)octet).ToString();
        }
    }
}
