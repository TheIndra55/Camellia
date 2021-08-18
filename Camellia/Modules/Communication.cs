using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace Camellia.Modules
{
    public class Communication : ModuleBase
    {
        [Command("Help")]
        public async Task HelpAsync()
        {
            await ReplyAsync(embed: new EmbedBuilder
            {
                Description =
                    "**Length [text]:** Give the length of a string\n" +
                    "**Hex [decimal number]:** Convert decimal to hexadecimal\n" +
                    "**Dec [hexadecimal number]:** Convert hexadecimal to decimal\n",
                Color = Color.Blue
            }.Build());
        }
    }
}
