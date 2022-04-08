using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace VanguardBot
{
    internal class Program
    {
        private DiscordSocketClient _client;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            //_client.Log += Log;
            await _client.LoginAsync(TokenType.Bot,
                Environment.GetEnvironmentVariable("DiscordToken"));
            await _client.StartAsync();
            ulong id = ulong.Parse(Environment.GetEnvironmentVariable("DiscordChannel"));

            await Task.Delay(-1); //needed to keep the program open until you manually stop the program
        }

    }
}
