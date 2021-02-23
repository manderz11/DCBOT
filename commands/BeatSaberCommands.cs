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

public class BeatSaberCommands : BaseCommandModule
{
    [Command("pp")]
    public async Task CuteCommand(CommandContext ctx, [RemainingText] string urll)
    {
        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = web.Load(urll);

        var span = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div/div[1]/div/div[2]/ul/li[2]");
        var pp = span.InnerText;
        await ctx.RespondAsync(pp);
    }
}