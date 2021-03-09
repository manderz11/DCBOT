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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DCBOT
{
    public class Program
    {
        public static string globalPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public static string globalConfig = @$"{globalPath}\Config";
        public static string globalExtraFiles = $@"{globalPath}\extrafiles";
        public static string ownerUsername = getConfig("Owner");


        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static string getConfig(string type)
        {
            string path = globalConfig + ".json";
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
                var jsonval = Convert.ToString(userObj[type]);
                return jsonval;
            }
            else if (!File.Exists(path))
            {
                defaultconfig f = new defaultconfig()
                {
                    Token = "TokenHere",
                    Owner = "Owner username here"
                };
                string stringjson = JsonConvert.SerializeObject(f);
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine(stringjson.ToString());
                    tw.Close();
                    Console.WriteLine("config created in directory, go configure it.");
                    Console.WriteLine("app will shutdown after enter...");
                    Console.Read();
                    Environment.Exit(1);
                    return string.Empty;
                }
            }
            else
            {
                return "fail";
            }
        }
        static async Task MainAsync()
        {
            var discord = new DiscordClient(new DiscordConfiguration()
            {
                Token = getConfig("Token"),
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

            cmd.RegisterCommands(Assembly.GetExecutingAssembly());
            await discord.ConnectAsync();
            await Task.Delay(-1);

        }
    }
    class defaultconfig
    {
        public string Token { get; set; }
        public string Owner { get; set; }
    }
}
