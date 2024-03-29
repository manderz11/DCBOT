﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;

public class Time : BaseCommandModule
{
    [Command("time")]
    [Description("Shows the bot owner's server/machines current local time")]
    public async Task TimeCommand(CommandContext ctx)
    {
        await ctx.RespondAsync(DateTime.Now.ToString());
    }
}