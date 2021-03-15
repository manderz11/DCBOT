using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

public class profile : BaseCommandModule
{
    [Command("profile")]
    [Description("Shows the given score saber profile description (performance points, global ranking, play count and total score)")]
    public async Task CuteCommand(CommandContext ctx, [RemainingText][Description("Score saber profile link")] string urll)
    {
        HtmlWeb web = new HtmlWeb();
        HtmlDocument doc = web.Load(urll);

        var span = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div/div[1]/div/div[2]/ul/li[2]");
        var pp = span.InnerText;

        var ranking = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div/div[1]/div/div[2]/ul/li[1]/a[1]");
        var rank = ranking.InnerText;

        var playcount = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div/div[1]/div/div[2]/ul/li[3]");
        var pcount = playcount.InnerText;

        var score = doc.DocumentNode.SelectSingleNode("/html/body/div/div/div/div[1]/div/div[2]/ul/li[4]");
        var score1 = score.InnerText;

        await ctx.RespondAsync($"this profiles stats are: {pp}, {pcount}, {score1} and is {rank} in the world.");
    }
}