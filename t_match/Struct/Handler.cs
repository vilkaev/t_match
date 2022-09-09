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
        public string FunctionHandler(Request request)
        {
            var teams = CalculateBody(request.Body) ;
            return JsonConvert.SerializeObject(teams);
        }

        public Response CalculateBody(string playersSet = "")
        {
            List<Player> initBascket;
            List<Player> basket;
            int teamsCount = 2;

            if (string.IsNullOrEmpty(playersSet))
            {
                initBascket = Player.CreateTestBasket(12);
            }
            else
            {
                //initBascket = JsonSerializer.Deserialize<List<Player>>(playersSet);
                var set = JsonConvert.DeserializeObject<RequestSet>(playersSet);
                initBascket = set.Players;
                teamsCount = set.TeamsCount;
            }

            basket = new List<Player>(initBascket);
            char[] letters = "ABCDEFG".ToCharArray();


            List<Team> teams = new List<Team>();
            for (int i = 0; i < teamsCount; i++)
                teams.Add(new Team() { Name = letters[i] });

            while (basket.Count != 0)
            {
                teams.ForEach(c =>
                {
                    if (basket.Count > 0)
                        c.Players.Add(SelectPlayerFromBasket(c, basket));
                }
                );


            }
            teams.ForEach(c => c.GetTeamScore());
            string result = JsonConvert.SerializeObject(teams);
            return new Response() { Teams = teams };

        }

        Player SelectPlayerFromBasket(Team team, List<Player> basket)
        {
            int maxScore = -1;
            int choosenIndex = -1;

            // good keeper is keeper rated > 3
            bool hasKeeper = team.Players.Any(c => c.Propes[PropTypes.Keeper] > 6);

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