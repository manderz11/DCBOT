using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using DCBOT;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.VoiceNext;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace DCBOT
{
    public class Program
    {
        // path example - C:/ProgramFiles/DCbot
        public static string globalPath = @"FullPath";
        public static string jsonPath = $@"{globalPath}\CustomJson";
        // your discord username or owners username
        // not compatible with multiple usernames yet!
        public static string ownerUsername = "Username";

        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static string getToken()
        {
            string path = $@"{jsonPath}\Token.json";
            if (File.Exists(path))
            {
                string result = string.Empty;
                using (StreamReader r = new StreamReader(path))
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
                var jsonvalue = Convert.ToString(userObj["jsonvalue"]);
                return jsonvalue;
            }
            else
            {
               Console.WriteLine("no token json, storing in source code is unsafe if published");
               return string.Empty; 
            }
        }

        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = getToken(),
                TokenType = TokenType.Bot,
                MinimumLogLevel = LogLevel.Debug
            });

            var cmd = discord.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "$" }
            });

            var interact = discord.UseInteractivity(new InteractivityConfiguration()
            {

            });

            var voicasync =  discord.UseVoiceNext(new VoiceNextConfiguration()
            {

            });

            cmd.RegisterCommands(Assembly.GetExecutingAssembly());
            await discord.ConnectAsync();
            await Task.Delay(-1);

        }
    }
}
