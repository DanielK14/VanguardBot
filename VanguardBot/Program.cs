using System;
using System.Threading.Tasks;
using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace VanguardBot
{
    internal class Program
    {
        private DiscordSocketClient _client;
        readonly ulong guildID = ulong.Parse(Environment.GetEnvironmentVariable("guildID"));
        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            await _client.LoginAsync(TokenType.Bot,
                Environment.GetEnvironmentVariable("DiscordToken"));
            await _client.StartAsync();
            _client.Ready += Client_Ready;
            _client.SlashCommandExecuted += SlashCommandHandler;
            

            await Task.Delay(-1); //needed to keep the program open until you manually stop the program
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        public async Task Client_Ready()
        {
            var statusCommand = new SlashCommandBuilder();
            statusCommand.WithName("status");
            statusCommand.WithDescription("Get the latest tweet from BungieHelp twitter.");

            var nightfallCommand = new SlashCommandBuilder();
            nightfallCommand.WithName("nightfall");
            nightfallCommand.WithDescription("Get the current Nightfall");

            var challengesCommand = new SlashCommandBuilder();
            challengesCommand.WithName("challenges");
            challengesCommand.WithDescription("Get the active raid challenges for this week");

            try
            {
                await _client.Rest.CreateGuildCommand(statusCommand.Build(), guildID);
                await _client.Rest.CreateGuildCommand(nightfallCommand.Build(), guildID);
                await _client.Rest.CreateGuildCommand(challengesCommand.Build(), guildID);
            }
            catch (HttpException exception)
            {
                // If our command was invalid, we should catch an ApplicationCommandException. This exception contains the path of the error as well as the error message. You can serialize the Error field in the exception to get a visual of where your error is.
                var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
                Console.WriteLine(json);
            }
        }
        private async Task SlashCommandHandler(SocketSlashCommand command)
        {
            switch (command.Data.Name)
            {
                case "status":
                    new TwitterMethods().TwitterMain(command);
                    break;
                case "nightfall":
                    new BungieMethods().getNightfall(command);
                    break;
                case "challenges":
                    new BungieMethods().getChallenges(command);
                    break;
                default:
                    await command.RespondAsync($"You have run {command.Data.Name}, which does nothing.");
                    break;
            }

        }
    }
}
