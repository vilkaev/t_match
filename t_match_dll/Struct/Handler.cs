using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Newtonsoft.Json;


namespace t_match
{

    public class Handler
    {
        public Response FunctionHandler(Request request)
        {
            try
            {
                return new Response(200, CalculateBody(request.Body)); 
            } catch (Exception ex)
            {
                return new Response(500, ex.Message);
            }
            
        }

        public string CalculateBody(string playersSet = "", int teamsCount = 2, int basketSize = 10)
        {
            List<Player> initBascket;
            List<Player> basket;

            if (string.IsNullOrEmpty(playersSet))
            {
                initBascket = Player.CreateTestBasket(basketSize);
            }
            else
            {
                //initBascket = JsonSerializer.Deserialize<List<Player>>(playersSet);
                initBascket = (JsonConvert.DeserializeObject<RequestSet>(playersSet)).Players;

            }

            basket = new List<Player>(initBascket);
            char[] letters = "АБВГД".ToCharArray();


            List<Team> teams = new List<Team>();
            for (int i = 0; i < teamsCount; i++)
                teams.Add(new Team() { Name = letters[i] });

            while (basket.Count != 0)
            {
                teams.ForEach(c =>
                {
                    if (basket.Count > 0)
                        c.players.Add(SelectPlayerFromBasket(c, basket));
                }
                );


            }
            teams.ForEach(c => c.GetTeamScore());

            //var options = new JsonSerializerOptions
            //{
            //    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            //    WriteIndented = true
            //};
            string result = JsonConvert.SerializeObject(teams);
            return result;

        }

        Player SelectPlayerFromBasket(Team team, List<Player> basket)
        {
            int maxScore = -1;
            int choosenIndex = -1;

            // good keeper is keeper rated > 3
            bool hasKeeper = team.players.Any(c => c.Propes[PropTypes.Keeper] > 6);

            foreach (Player player in basket)
            {
                var playScoreForTeam = 0;
                foreach (var item in player.Propes)
                {
                    playScoreForTeam += item.Value * (int)(team.MyWeiths is null ? 1 : team.MyWeiths[item.Key]);
                };

                if (playScoreForTeam > maxScore)
                {
                    maxScore = playScoreForTeam;
                    choosenIndex = basket.IndexOf(player);
                }
            }

            Player selectedPlayer = basket[choosenIndex];

            basket.RemoveAt(choosenIndex);

            return selectedPlayer;
        }
    }

}