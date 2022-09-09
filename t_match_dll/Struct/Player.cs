using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace t_match
{
    public class Player
    {
        public string PlayerID { get; set; }
        public int Score { get; set; }

        public Dictionary<PropTypes, int> Propes { get; set; } = new Dictionary<PropTypes, int>();

        public Player(string id, int _b, int _sh, int _st, int _kee)
        {
            PlayerID = id;
            Propes.Add(PropTypes.Boal, _b);
            Propes.Add(PropTypes.Shot, _sh);
            Propes.Add(PropTypes.Stamina, _st);
            Propes.Add(PropTypes.Keeper, _kee);

            Score = Propes.Sum(c => c.Value);
        }

        public static List<Player> CreateTestBasket(int num)
        {
            Random rnd = new Random();

            List<Player> players = new List<Player>();

            for (int i = 0; i < num; i++)
            {
                players.Add(new Player(RandomString(4), rnd.Next(1, 10), rnd.Next(1, 10), rnd.Next(1, 10), rnd.Next(1, 10)));
            }

            return players;
        }

        public static string RandomString(int length)
        {
            Random rnd = new Random();
            const string chars1 = "ВГДКЛМНХ";
            const string chars2 = "АОЭЮЯИЕЫ";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                if (i % 2 == 0)
                    sb.Append(chars1[rnd.Next(chars1.Length)]);
                else
                    sb.Append(chars2[rnd.Next(chars1.Length)]);

            }

            return sb.ToString();

            //var strnew string(Enumerable.Repeat(chars1, length/2)
            //    .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

    }

    public struct Prop
    {
        public PropTypes Name { get; set; }
        public int Val { get; set; }
        public double Weight { get; set; }

    }

    public enum PropTypes
    {
        Boal,
        Shot,
        Stamina,
        Keeper
    }


}
