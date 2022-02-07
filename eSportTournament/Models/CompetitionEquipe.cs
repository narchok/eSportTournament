using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eSportTournament.Models
{
    public class CompetitionEquipe
    {
        public int ID { get; set; }
        public int competitionID { get; set; }
        public Competition CompetitionCs { get; set; }
        public int equipeID { get; set; }
        public Equipe EquipeE { get; set; }
    }
}
