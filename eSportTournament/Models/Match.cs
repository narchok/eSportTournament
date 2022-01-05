using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eSportTournament.Models
{
    public class Match
    {
        public int ID { get; set; }
        [Required]

        [ForeignKey("Equipe")]
        public int? EquipeAID { get; set; }
        [ForeignKey("Equipe")]
        public int? EquipeBID { get; set; }

        public Equipe EquipeA { get; set; }
        public Equipe EquipeB { get; set; }

        public int? EquipeAScore { get; set; }
        public int? EquipeBScore { get; set; }

        public int? gagnantID { get; set; }

        [ForeignKey("Competition")]
        public int CompetitionID { get; set; }

        public Competition Competition { get; set; }



    }
}
