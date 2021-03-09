using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DCBOT;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;

public class Log : BaseCommandModule
{
    [Command("log")]
    [Description("Sends a log in console")]
    public async Task CuteCommand(CommandContext ctx,[RemainingText][Description("The log message")] string message)
    {
        if (ctx.User.Username == Program.ownerUsername)
        {
            ctx.Client.Logger.LogWarning(message);

            await ctx.RespondAsync("Sent log message!");
        }
        else
        {
            await ctx.RespondAsync("Invalid permissions!");
        }

    }
}