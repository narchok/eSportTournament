using eSportTournament.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSportTournament.VueModel
{
    public class ClasssementChampionnat
    {
        public Equipe equipe { get; set; }

        public int nombreMatcheJouer { get; set; }

        public int nombreVictoire { get; set; }
        public int nombreDefaite { get; set; }
        public int points { get; set; }


    }
}
