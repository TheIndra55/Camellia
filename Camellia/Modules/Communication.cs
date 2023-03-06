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
                    "**Dec [hexadecimal number]:** Convert hexadecimal to decimal\n" +
                    "**JSON [text]:** Check if a JSON is valid or not\n" +
                    "**XML [text]:** Check if an XML is valid or not\n" +
                    "**Calc [operation]:** Evaluate a mathematical expression\n" +
                    "**Bytes [number of bytes]:** Generates a string of cryptographic random bytes\n" +
                    "**Duration [date 1] [date 2]:** Display the duration between 2 dates\n" +
                    "**Invite** Get the invite link of the bot",
                Color = Color.Blue,
                Footer = new EmbedFooterBuilder
                {
                    Text = "Any other question? Feel free to open an issue: https://github.com/Xwilarg/Camellia/"
                }
            }.Build());
        }

        [Command("Invite")]
        public async Task InviteAsync()
        {
            await ReplyAsync($"https://discord.com/api/oauth2/authorize?client_id={Context.Client.CurrentUser.Id}&scope=bot");
        }
    }
}
