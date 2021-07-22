using Discord.Commands;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Camellia.TypeReader
{
    public sealed class HexReader : Discord.Commands.TypeReader
    {
        public override async Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            if (input.ToUpperInvariant().StartsWith("0X"))
            {
                input = input[2..];
            }
            if (!int.TryParse(input, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int res))
            {
                return TypeReaderResult.FromError(CommandError.ParseFailed, "Hexadecimal numbers must be provided");
            }
            return TypeReaderResult.FromSuccess(new Hex() { Value = res });
        }
    }
}
