using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace BoVoyageFinal.Models
{
    public class ResaViewModel
    {       
        
        public int VoyageId { get; set; }

        public string NomDestination { get; set; }

        public List<Personne> Participants { get; set; }

        public Personne CurrentPerson { get; set; }

        public ResaViewModel()
        {
            Participants = new List<Personne>();
        }

    }
}
