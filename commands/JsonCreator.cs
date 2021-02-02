using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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
using Newtonsoft.Json;

public class JsonCreator : BaseCommandModule
{
    [Command("jsoncreator")]
    public async Task CuteCommand(CommandContext ctx, string jsonname, string jsonvalue)
    {
        if (ctx.User.Username == Program.ownerUsername || ctx.User.Username == "SomeOtherUsername")
        {
            ctx.Client.Logger.LogInformation("Attempting creation of json");
            await ctx.RespondAsync("Attempting creation");
            await ctx.RespondAsync("Json will be stored, and accessible after restart");
            storeInfoWOfunc storeinfo = new storeInfoWOfunc()
                    {
                        jsonvalue = jsonvalue,
                        jsonname = jsonname,
                    };
                    ctx.Client.Logger.LogInformation("MadeJson");
                    await ctx.RespondAsync("Json made");
                    string stringjson = JsonConvert.SerializeObject(storeinfo);
                    string path = Program.jsonPath + jsonname +".json";
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                        using (var tw = new StreamWriter(path, true))
                        {
                            tw.WriteLine(stringjson.ToString());
                            tw.Close();
                        }
                    }else if (!File.Exists(path))
                    {
                        using (var tw = new StreamWriter(path, true))
                        {
                            tw.WriteLine(stringjson.ToString());
                            tw.Close();
                        }
                    }

        }
        else
        {
            await ctx.RespondAsync("Invalid permissions!");
        }

    }
}

class storeInfoWOfunc
{
    public string jsonname { get; set; }
    public string jsonvalue { get; set; }
}