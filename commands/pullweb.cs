using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using HtmlAgilityPack;

public class pullweb : BaseCommandModule
{
    [Command("pullweb")]
    [Description("Pulls the XPath (found in inspect element-copy) and displays it")]
    public async Task CuteCommand(CommandContext ctx, [Description("The url")]string urll, [RemainingText][Description("The object/value path")] string xPath)
    {
        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = web.Load(urll);

        var span = doc.DocumentNode.SelectSingleNode($"{xPath}");
        var pull = span.InnerText;
        await ctx.RespondAsync($"the output was: {pull}");
    }
}