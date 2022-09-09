using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t_match
{
    public class Team
    {
        public char Name { get; set; }
        public int AvrScore { get; set; }
        public List<Player> players { get; set; } = new List<Player>();

        public Dictionary<PropTypes, double> MyWeiths { get; set; }

        public Dictionary<PropTypes, int> PropsScore { get; set; }

    

        public Team()
        {
            MyWeiths = new Dictionary<PropTypes, double>();
            MyWeiths.Add(PropTypes.Keeper, 1);
            MyWeiths.Add(PropTypes.Boal, 1);
            MyWeiths.Add(PropTypes.Shot, 1);
            MyWeiths.Add(PropTypes.Stamina, 1);
        }

        public void GetTeamScore()
        {
            PropsScore = new Dictionary<PropTypes, int>();
            foreach (var item in players[0].Propes)
            {
                PropsScore.Add(item.Key, (int)players.Average(c => c.Propes[item.Key]));
            }
            AvrScore = PropsScore.Sum(c => c.Value);
        }
    }

}
