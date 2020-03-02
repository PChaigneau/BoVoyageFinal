using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace BoVoyageFinal.Models
{
    public class ResaViewModel
    {       
        public Voyage Voyage { get; set; }

        public List<Voyageur> Participants { get; set; }

    }
}
