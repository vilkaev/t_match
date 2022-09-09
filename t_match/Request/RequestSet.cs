using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using t_match.Models;

namespace t_match
{
    public class RequestSet
    {
        public int TeamsCount { get; set; }
        public List<Player> Players { get; set; }
    }

}
