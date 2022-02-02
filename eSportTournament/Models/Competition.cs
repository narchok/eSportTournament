using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eSportTournament.Models
{
    public class Competition
    {
        [Required]
        public int ID { get; set; }
        [Required]

        public string nomCompetition { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }

        public int nombreJoueurs { get; set; }
        
        public bool isTournoi { get; set; }

        [DefaultValue(false)]
        public bool terminer { get; set; }

        public ICollection<CompetitionEquipe> CompetitionsEquipe { get; set; }

        //public List<Match> Matchs { get; set; }



    }
}
