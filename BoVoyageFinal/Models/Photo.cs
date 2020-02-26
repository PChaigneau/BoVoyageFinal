using System;
using System.Collections.Generic;

namespace BoVoyageFinal.Models
{
    public partial class Photo
    {
        public int Id { get; set; }
        public string NomFichier { get; set; }
        public int IdDestination { get; set; }

        public virtual Destination IdDestinationNavigation { get; set; }
    }
}
