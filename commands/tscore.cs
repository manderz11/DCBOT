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

public class tscore : BaseCommandModule
{
    [Command("tscore")]
    [Description("Pulls the top score from Score Saber")]
    public async Task CuteCommand(CommandContext ctx, [RemainingText][Description("The Score Saber profile url")] string urll)
    {
        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = web.Load($"{urll}");

        var thing = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div/div[2]/div[2]/table/tbody/tr[1]/th[2]/div/div[2]/a/span[1]");
        var songname = thing.InnerText;

        var thing2 = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div/div[2]/div[2]/table/tbody/tr[1]/th[3]/span[1]");
        var ppgain = thing2.InnerText;

        var acc = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div/div[2]/div[2]/table/tbody/tr[1]/th[3]/span[4]");
        var acc1 = acc.InnerText;

        await ctx.RespondAsync($"The top scored song played is- {songname} with the pp gain of {ppgain}pp (not weighted) with {acc1}");
    }
}