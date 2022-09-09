// See https://aka.ms/new-console-template for more information
using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Newtonsoft.Json;

using System.Text.Unicode;
using t_match;

Console.WriteLine("Started calculation");

string r = File.ReadAllText(@"D:\gt_loc\t_match\t_match\example.json");
Request Request = new Request() { Body = r };
string r1 = JsonConvert.SerializeObject(Request);
File.WriteAllText(@"D:\gt_loc\t_match\t_match\exampleFull.json", r1);
return;



string res = CalculateBody();

RequestSet set = new RequestSet() { Players = Player.CreateTestBasket(6), TeamsCount = 2 };

File.WriteAllText(@"D:\gt_loc\t_match\t_match\out.json", res);

string CalculateBody(string playersSet = "", int teamsCount = 2, int basketSize = 10)
{
    List<Player> initBascket;
    List<Player> basket;

    if (string.IsNullOrEmpty(playersSet))
    {
        initBascket  = Player.CreateTestBasket(basketSize);
    }
    else
    {
        initBascket = JsonConvert.DeserializeObject<List<Player>>(playersSet);
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
                c.Players.Add(SelectPlayerFromBasket(c, basket));
        }
        );


    }
    teams.ForEach(c => c.GetTeamScore());

    //var options = new JsonSerializerOptions
    // {
    //     Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
    //     WriteIndented = true
    // };
    string result = JsonConvert.SerializeObject(teams);
    return result;

}

Player SelectPlayerFromBasket(Team team, List<Player> basket)
{
    int maxScore = -1;
    int choosenIndex = -1;

    // good keeper is keeper rated > 3
    bool hasKeeper = team.Players.Any(c => c.Propes[PropTypes.Keeper] > 6);
    
    // if have keeper its weight has no meaning;
    //if (hasKeeper)
    //{
    //    team.MyWeiths[PropTypes.Keeper] = 0;
    //    team.MyWeiths[PropTypes.Boal] = 2;
    //}
    //else
    //    team.MyWeiths[PropTypes.Keeper] = 5; 

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

