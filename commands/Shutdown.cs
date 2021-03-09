using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DCBOT;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;

public class Shutdown : BaseCommandModule
{
    [Command("shutdown")]
    [Description("Shuts the bot down")]
    public async Task CuteCommand(CommandContext ctx)
    {
        if (ctx.User.Username == Program.ownerUsername)
        {
            await ctx.RespondAsync("Shutting down bot!");
            Environment.Exit(1);
        }
        else
        {
            await ctx.RespondAsync("Invalid permissions!");
        }
        
    }
}

