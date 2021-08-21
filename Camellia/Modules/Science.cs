using Dangl.Calculator;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Camellia.Modules
{
    public class Science : ModuleBase
    {
        [Command("Calc")]
        public async Task CalcAsync([Remainder]string str)
        {
            await ReplyAsync(Calculator.Calculate(str).Result.ToString());
        }

        [Command("Json")]
        public async Task JsonAsync([Remainder]string str = null)
        {
            str = await GetCleanInputCodeAsync("json", str);
            if (str == null)
            {
                await ReplyAsync("You must provide the text to parse, either as an argument, as an attachment or as the previous message by giving \"^\" as a parameter.");
                return;
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
        public async Task XmlAsync([Remainder] string str = null)
        {
            str = await GetCleanInputCodeAsync("xml", str);
            if (str == null)
            {
                await ReplyAsync("You must provide the text to parse, either as an argument, as an attachment or as the previous message by giving \"^\" as a parameter.");
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
            if (str == null)
            {
                if (Context.Message.Attachments.Any()) // Empty message but contains an attachment
                {
                    return await StaticObjects.HttpClient.GetStringAsync(Context.Message.Attachments.ElementAt(0).Url);
                }
                return null;
            }
            if (str == "^") // We need to check previous message
            {
                var msg = await GetLastMessageAsync();
                if (msg.Attachments.Any()) // Previous message has an attachment
                {
                    return await StaticObjects.HttpClient.GetStringAsync(msg.Attachments.ElementAt(0).Url);
                }
                else if (string.IsNullOrWhiteSpace(msg.Content)) // Previous message is empty
                {
                    return null;
                }
                str = msg.Content;
            }

            // Check for code tags
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

        private async Task<IMessage> GetLastMessageAsync()
        {
            var msgs = await Context.Channel.GetMessagesAsync(2).FlattenAsync();
            if (msgs.Count() == 2)
            {
                return msgs.ElementAt(1);
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
