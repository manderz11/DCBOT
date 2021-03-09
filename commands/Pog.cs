using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DCBOT;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;

public class Pog : BaseCommandModule
{
    [Command("pog")]
    [Description("Displays Pog trough a image")]
    public async Task CuteCommand(CommandContext ctx)
    {
        var msg = await new DiscordMessageBuilder()
            .WithContent("Pog")
            .WithFile(@"extrafiles\Pog.png")
            .SendAsync(ctx.Channel);
    }
}

