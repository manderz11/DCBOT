using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;

public class Sounds : BaseCommandModule
{
    [Command("join")]
    public async Task JoinCommand(CommandContext ctx, DiscordChannel channel = null)
    {
        channel ??= ctx.Member.VoiceState?.Channel;
        if (channel == null)
        {
            await ctx.RespondAsync("You are not in a channel >: (");
        }
        else
        {
            await channel.ConnectAsync();
        }
    }

    [Command("play")]
    [Description("Plays the given song that is located on host")]
    public async Task PlayCommand(CommandContext ctx, string path)
    {
        var vnext = ctx.Client.GetVoiceNext();
        var connection = vnext.GetConnection(ctx.Guild);

        var transmit = connection.GetTransmitSink();
        if (path == null || path == "")
        {
            await ctx.RespondAsync("No files >: (");
        }
        else
        {
            if (File.Exists(path))
            {
                var pcm = ConvertAudioToPcm(path);
                await pcm.CopyToAsync(transmit); 
                await pcm.DisposeAsync(); 
            }
            else
            {
                await ctx.RespondAsync("This doesnt exist >: (");
            }
        }
        
    }

    [Command("leave")]
    public async Task LeaveCommand(CommandContext ctx)
    {
        var vnext = ctx.Client.GetVoiceNext();
        var connection = vnext.GetConnection(ctx.Guild);

        connection.Disconnect();
    }

    private Stream ConvertAudioToPcm(string filePath)
    {
        var ffmpeg = Process.Start(new ProcessStartInfo
        {
            FileName = "ffmpeg",
            Arguments = $@"-i ""{filePath}"" -ac 2 -f s16le -ar 48000 pipe:1",
            RedirectStandardOutput = true,
            UseShellExecute = false
        });

        return ffmpeg.StandardOutput.BaseStream;
    }
}