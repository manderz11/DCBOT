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

public class rscore : BaseCommandModule
{
    [Command("rscore")]
    [Description("Displays the most recent score in PP (no score support yet) by pulling from the Score Saber profile")]
    public async Task CuteCommand(CommandContext ctx, [RemainingText][Description("Score Saber profile url")] string urll)
    {
        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = web.Load($"{urll}&sort=2");

        var thing = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div/div[2]/div[2]/table/tbody/tr[1]/th[2]/div/div[2]/a/span[1]");
        var songname = thing.InnerText;

        var thing2 = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div/div[2]/div[2]/table/tbody/tr[1]/th[3]/span[1]");
        var ppgain = thing2.InnerText;

        var acc = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div/div[2]/div[2]/table/tbody/tr[1]/th[3]/span[4]");
        var acc1 = acc.InnerText;

        await ctx.RespondAsync($"The most recent song played is- {songname} with the pp gain of {ppgain}pp (not weighted) with {acc1}");
    }
}