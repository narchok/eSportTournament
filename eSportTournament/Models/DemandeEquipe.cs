using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eSportTournament.Models
{
    public class DemandeEquipe
    {
        public int ID { get; set; }
        [Required]

        public string userID { get; set; }

        public int equipeID { get; set; }


        [DefaultValue(false)]
        public bool approuver { get; set; }

    }
}
