using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VanguardBot
{
    class BungieMethods
    {

        public async void bungieTest()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", Environment.GetEnvironmentVariable("BungieAPIToken"));

                var response = await client.GetAsync("https://www.bungie.net/platform/Destiny/Manifest/InventoryItem/1274330687/");
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                Console.WriteLine(item.Response.data.inventoryItem.itemName); //Gjallarhorn
            }
        }

        public async void getNightfall(SocketSlashCommand command)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", Environment.GetEnvironmentVariable("BungieAPIToken"));

                var response = await client.GetAsync("https://www.bungie.net/Platform/Destiny2/3/Profile/4611686018484533920/Character/2305843009460474089?components=204");
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                long NFHash = item.Response.activities.data.availableActivities[16].activityHash;
                int NFId = hashConverter(NFHash);
                string nightFallName = NFLookup(NFId);
                await command.RespondAsync(nightFallName);
            }
        }
        public async void getChallenges(SocketSlashCommand command)
        {
            List<int> activitiesId = new List<int> { 18, 36, 69, 73 };
            string challenges = "";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-API-Key", Environment.GetEnvironmentVariable("BungieAPIToken"));
                var response = await client.GetAsync("https://www.bungie.net/Platform/Destiny2/3/Profile/4611686018484533920/Character/2305843009460474089?components=204");
                var content = await response.Content.ReadAsStringAsync();
                dynamic item = Newtonsoft.Json.JsonConvert.DeserializeObject(content);
                foreach(int activity in activitiesId)
                {
                    long challengeHash = item.Response.activities.data.availableActivities[activity].modifierHashes[0];
                    int challengeId = hashConverter(challengeHash);
                    string challengeName = challengeLookUp(challengeId);
                    challenges += challengeName + ", ";
                }
                await command.RespondAsync(challenges);
            }
        }

        private int hashConverter(long hash)
        {
            var id = unchecked((int) hash);
            return id;
        }
        private string NFLookup(int id)
        {
            switch (id)
            {
                case -1878652904:
                    return "The Corrupted";
                case -1695965378:
                    return "The Inverted Spire";
                case -1528122989:
                    return "Birthplace of the Vile";
                case -1265578591:
                    return "The Insight Terminus";
                case -1185773727:
                    return "Lake of Shadows";
                case -1061468841:
                    return "Exodus Crash";
                case -1001337163:
                    return "Fallen S.A.B.E.R.";
                case -482831846:
                    return "The Glassway";
                case -98022931:
                    return "Warden of Nothing";
                case 265186824:
                    return "Broodhold";
                case 557845335:
                    return "Warden of Nothing";
                case 1203950593:
                    return "The Devels' Lair";
                case 1495545957:
                    return "The Scarlet Keep";
                case 1561733171:
                    return "The Hollowed Lair";
                case 1753547900:
                    return "The Arms Dealer";
                case 1964120204:
                    return "The Lightblade";
                case 2103025314:
                    return "Proving Grounds";
                case 2136458561:
                    return "The Disgraced";
                default:
                    return "Unknown NightfallId";

            }
        }

        private string challengeLookUp(int id)
        {
            switch (id)
            {
                case -2038982596:
                    return "Defences Down";
                case -1668962583:
                    return "To the Top";
                case -1622741174:
                    return "Swift Destruction";
                case -1480120887:
                    return "Out of It's Way";
                case -1159501237:
                    return "Wait for It...";
                case -1082245710:
                    return "Strangers in Time";
                case -933069936:
                    return "Copies of Copies";
                case -869445776:
                    return "Base Information";
                case -271344164:
                    return "Red Rover";
                case -221770195:
                    return "Zero to One Hundred";
                case 191124900:
                    return "The Core Four";
                case 201968501:
                    return "Of All Trades";
                case 274420331:
                    return "The Only Oracle For You";
                case 584466411:
                    return "Leftovers";
                case 787678752:
                    return "A Link to the Chain";
                case 1572570117:
                    return "Ensemble's Refrain";
                case 2098788044:
                    return "Looping Catalyst";
                default:
                    return "Unknown ChallengeId";

            }
        }
    }
}
