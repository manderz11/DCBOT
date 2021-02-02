using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using DCBOT;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Builders;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class JsonLoader : BaseCommandModule
{
    [Command("jsonloader")]
    public async Task CuteCommand(CommandContext ctx, [RemainingText] string message)
    {
        if (ctx.User.Username == Program.ownerUsername || ctx.User.Username == "SomeOtherUsername")
        {
            await ctx.RespondAsync("loading!");
            string path = @"D:\Programing\projectexe\DCbot\CustomJson";
            string[] filePaths = Directory.GetFiles(path, "*.json");
            for (int i = 0; i < filePaths.Length; i++)
            {
                await ctx.RespondAsync(filePaths[i]);
                string filepath = filePaths[i];
                string result = string.Empty;
                using (StreamReader r = new StreamReader(filepath))
                {
                    var json = r.ReadToEnd();
                    var jobj = JObject.Parse(json);
                    foreach (var item in jobj.Properties())
                    {
                        item.Value = item.Value.ToString().Replace("v1", "v2");
                    }
                    result = jobj.ToString();
                }
                var userObj = JObject.Parse(result);
                var jsonname = Convert.ToString(userObj["jsonname"]);
                var jsonvalue = Convert.ToString(userObj["jsonvalue"]);
                if (jsonname != "Token")
                {
                    await ctx.RespondAsync(jsonname);
                    await ctx.RespondAsync(jsonvalue);
                }
                else
                {
                    await ctx.RespondAsync("no token 4 u :D");
                }
            }
        }
        else
        {
            await ctx.RespondAsync("Invalid permissions!");
        }

    }
}