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
    public async Task CuteCommand(CommandContext ctx)
    {
        await ctx.RespondWithFileAsync(@"extrafiles\Pog.png");
    }
}

