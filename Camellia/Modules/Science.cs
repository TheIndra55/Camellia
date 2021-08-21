using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Camellia.Modules
{
    public class Science : ModuleBase
    {
        [Command("Json")]
        public async Task JsonAsync([Remainder]string str)
        {
            if (str.StartsWith("```json") && str.EndsWith("```"))
            {
                str = str[7..^3];
            }
            else if (str.StartsWith("```") && str.EndsWith("```"))
            {
                str = str[3..^3];
            }
            try
            {
                JsonConvert.DeserializeObject(str);
                await ReplyAsync("Your JSON is valid");
            }
            catch (JsonReaderException e)
            {
                await ReplyAsync("Your JSON is **not** valid:\n```" + e.Message + "\n```");
            }
        }

        [Command("XML")]
        public async Task XmlAsync([Remainder] string str)
        {
            if (str.StartsWith("```xml") && str.EndsWith("```"))
            {
                str = str[6..^3];
            }
            else if (str.StartsWith("```") && str.EndsWith("```"))
            {
                str = str[3..^3];
            }
            var xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(str);
                await ReplyAsync("Your XML is valid");
            }
            catch (XmlException e)
            {
                await ReplyAsync("Your XML is **not** valid:\n```" + e.Message + "\n```");
            }
        }

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
        public async Task HexAsync(params int[] numbers)
        {
            await ReplyAsync(string.Join(" ", numbers.Select(x => x.ToString("X"))));
        }

        [Command("Dec")]
        public async Task DecAsync(params Hex[] numbers)
        {
            await ReplyAsync(string.Join(" ", numbers.Select(x => x.Value)));
        }
    }
}
