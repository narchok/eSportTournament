using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eSportTournament.Models
{
    public class Equipe
    {
        public int ID { get; set; }
        [Required]
        public string nomEquipe { get; set; }

        public ICollection<Utilisateur> Joueurs { get; set; }


        [DefaultValue(false)]
        public bool valider { get; set; }

      

    }
}
