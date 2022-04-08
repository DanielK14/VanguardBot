using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterSharp;

namespace VanguardBot
{
    class TwitterMethods
    {
        readonly string twitterToken = Environment.GetEnvironmentVariable("BearerToken");
        private TwitterSharp.Client.TwitterClient _client;

        public async void TwitterMain(SocketSlashCommand command)
        {
            _client = new TwitterSharp.Client.TwitterClient(twitterToken);
            var bungieHelp = await _client.GetUserAsync("BungieHelp");
            var answer = await _client.GetTweetsFromUserIdAsync(bungieHelp.Id);
            var tweet = answer[0];
            await command.RespondAsync(tweet.Text);           

        }

    }
}
