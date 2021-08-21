using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System;
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
            str = await GetCleanInputCodeAsync("json", str);
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
            str = await GetCleanInputCodeAsync("xml", str);
            if (str == null)
            {
                await ReplyAsync("There is no message above this one.");
                return;
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

        private async Task<string> GetCleanInputCodeAsync(string currentLanguage, string str)
        {
            if (str == "^")
            {
                str = await GetLastMessageAsync();
            }
            if (str.StartsWith("```" + currentLanguage, StringComparison.InvariantCultureIgnoreCase) && str.EndsWith("```"))
            {
                return str[(3 + currentLanguage.Length)..^3].Trim();
            }
            if (str.StartsWith("```") && str.EndsWith("```"))
            {
                return str[3..^3].Trim();
            }
            return str.Trim();
        }

        private async Task<string> GetLastMessageAsync()
        {
            var msgs = await Context.Channel.GetMessagesAsync(2).FlattenAsync();
            if (msgs.Count() == 2)
            {
                return msgs.ElementAt(1).Content;
            }
            return null;
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
