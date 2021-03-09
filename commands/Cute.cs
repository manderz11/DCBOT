using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;

public class Cute : BaseCommandModule
{
    [Command("cute")]
    [Description("Says the input name/mention is cute :3")]
    public async Task CuteCommand(CommandContext ctx, [RemainingText][Description("The object/name or mention")] string name)
    {
        await ctx.RespondAsync($"{name} is cute :3");
        await ctx.Message.DeleteAsync();
    }
}
