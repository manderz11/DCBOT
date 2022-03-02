using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;
using Microsoft.Extensions.Logging;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;

public class Sounds : BaseCommandModule
{
    [Command("join")]
    [Description("Joins a voice channel for voice commands")]
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
        while (connection.IsPlaying)
        {
            await connection.WaitForPlaybackFinishAsync();
        }

        if (path == null || path == "")
        {
            await ctx.RespondAsync("No files >: (");
        } 
        else if (connection.IsPlaying == true)
        {
            await ctx.RespondAsync("Already playing!");
        }
        else
        {
            if (File.Exists(path))
            {
                var pcm = ConvertAudioToPcm(path);
                await pcm.CopyToAsync(transmit, cancellationToken: new CancellationToken());
                await pcm.DisposeAsync();
            }
            else
            {
                await ctx.RespondAsync("This doesnt exist >: (");
            }
        }
        
    }
    [Command("PlayFromURL")]
    [Description("Plays from given youtube url")]
    public async Task PlayFromURLCommand(CommandContext ctx, string URL)
    {
        var vnext = ctx.Client.GetVoiceNext();
        var connection = vnext.GetConnection(ctx.Guild);

        var transmit = connection.GetTransmitSink();

        if (connection.IsPlaying == true)
        {
            await ctx.RespondAsync("Already playing!");
        }
        else
        {
            
            await ctx.RespondAsync("Starting download!");
            if (File.Exists(@"./audio.mp3"))
            {
                File.Delete(@"./audio.mp3");
            }
            var youtube = new YoutubeClient();

            await youtube.Videos.DownloadAsync(URL, "audio.mp3", o => o
                .SetPreset(ConversionPreset.UltraFast) // change preset
                .SetFFmpegPath("./ffmpeg.exe") // custom FFmpeg location
            );

            /* previous version
             * var streamManifest = await youtube.Videos.Streams.GetManifestAsync(ID);
            var streamInfo = streamManifest
                .GetAudioOnlyStreams()
                .Where(s => s.Container == Container.Mp3)
                .GetWithHighestBitrate();

            await youtube.Videos.Streams.DownloadAsync(streamInfo, $"audio.{streamInfo.Container}");*/

            await ctx.RespondAsync("Downloaded! Playing...");

            while (connection.IsPlaying)
            {
                await connection.WaitForPlaybackFinishAsync();
            }

            var pcm = ConvertAudioToPcm(@"./audio.mp3");
            await pcm.CopyToAsync(transmit, cancellationToken: new CancellationToken());
            await pcm.DisposeAsync();
        }
    }
    [Command("stop")]
    [Description("Stops the current playback")]
    public async Task StopCommand(CommandContext ctx)
    {
        await ctx.RespondAsync("Stopping...");
        var vnext = ctx.Client.GetVoiceNext();
        var connection = vnext.GetConnection(ctx.Guild);
        var transmit = connection.GetTransmitSink();
        if (connection.IsPlaying)
        {
            if (processId != 0)
            {
                Process process = Process.GetProcessById(processId);
                process.Kill();
            }
            
        }
    }
    [Command("pause")]
    [Description("Pauses currently playing song")]
    public async Task PauseCommand(CommandContext ctx, DiscordChannel channel = null)
    {
        var vnext = ctx.Client.GetVoiceNext();
        var connection = vnext.GetConnection(ctx.Guild);
        var transmit = connection.GetTransmitSink();
        if (connection.IsPlaying)
        {
            transmit.Pause();
        }
        else
        {
            await ctx.RespondAsync("Not playing in any channel!");
        }
    }
    [Command("resume")]
    [Description("Resumes a paused song")]
    public async Task ContinueCommand(CommandContext ctx, DiscordChannel channel = null)
    {
        var vnext = ctx.Client.GetVoiceNext();
        var connection = vnext.GetConnection(ctx.Guild);
        var transmit = connection.GetTransmitSink();
        await transmit.ResumeAsync();
    }
    [Command("volume")]
    [Description("Changes the volume of the currently playing song (100 is default)")]
    public async Task VolumeCommand(CommandContext ctx, [RemainingText] double volume)
    {
        var vnext = ctx.Client.GetVoiceNext();
        var connection = vnext.GetConnection(ctx.Guild);
        var transmit = connection.GetTransmitSink();
        if (connection.IsPlaying)
        {
            double we = transmit.VolumeModifier * 100;
            transmit.VolumeModifier = volume / 100;
            await ctx.RespondAsync($"Changed volume to {transmit.VolumeModifier * 100} from the previous {we}");
        }
        else
        {
            await ctx.RespondAsync("Not playing in any channel!");
        }
    }
    [Command("leave")]
    [Description("Leaves the current voice channel")]
    public async Task LeavingCommand(CommandContext ctx)
    {
        var vnext = ctx.Client.GetVoiceNext();
        var connection = vnext.GetConnection(ctx.Guild);
        connection.Disconnect();
        await ctx.RespondAsync("ok : (");
    }
    public int processId = 0;
    private Stream ConvertAudioToPcm(string filePath)
    {
        var ffmpeg = Process.Start(new ProcessStartInfo
        {
            FileName = "ffmpeg",
            Arguments = $@"-i ""{filePath}"" -ac 2 -f s16le -ar 48000 pipe:1",
            RedirectStandardOutput = true,
            UseShellExecute = false
        });

        processId = ffmpeg.Id;
        return ffmpeg.StandardOutput.BaseStream;
    }
}