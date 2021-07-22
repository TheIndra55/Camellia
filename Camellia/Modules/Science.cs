using Discord;
using Discord.Commands;
using System.Linq;
using System.Threading.Tasks;

namespace Camellia.Modules
{
    public class Science : ModuleBase
    {
        [Command("Length")]
        public async Task Length([Remainder]string str)
        {
            await ReplyAsync(embed: new EmbedBuilder
            {
                Color = Color.Blue,
                Fields = new()
                {
                    new()
                    {
                        Name = "Length",
                        Value = str.Length
                    },
                    new()
                    {
                        Name = "Alphanumeric only",
                        Value = str.Where(x => char.IsLetterOrDigit(x)).Count()
                    }
                }
            }.Build());
        }

        [Command("Hex")]
        public async Task Hex(params int[] numbers)
        {
            await ReplyAsync(string.Join(" ", numbers.Select(x => x.ToString("X"))));
        }

        [Command("Dec")]
        public async Task Dec(params Hex[] numbers)
        {
            await ReplyAsync(string.Join(" ", numbers.Select(x => x.Value)));
        }
    }
}
