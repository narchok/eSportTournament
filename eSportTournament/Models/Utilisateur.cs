using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eSportTournament.Models
{
    public class Utilisateur
    {
        [Required]
        public int ID { get; set; }

        public string nom { get; set; }

      //  public int EquipeID { get; set; }

       // public Equipe Equipe { get; set; }


    }
}
