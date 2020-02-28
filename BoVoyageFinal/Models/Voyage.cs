using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoVoyageFinal.Models
{
    public partial class Voyage
    {
        public Voyage()
        {
            Dossierresa = new HashSet<Dossierresa>();
            Voyageur = new HashSet<Voyageur>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public int IdDestination { get; set; }

        [Required]
        [Display(Name = "Date de retour")]
        [DataType(DataType.Date)]
        public DateTime DateDepart { get; set; }

        [Required]
        [Display(Name = "Date de retour")]
        [DataType(DataType.Date)]
        public DateTime DateRetour { get; set; }
        [Required]
        public int PlacesDispo { get; set; }
        [Required]
        public decimal PrixHt { get; set; }
        
        [Required]
        [Display(Name = "Réduction")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public decimal Reduction { get; set; }
        [Display(Name = "Descriptif")]
        public string Descriptif { get; set; }

        [Display(Name = "Destination")]
        public virtual Destination IdDestinationNavigation { get; set; }
        public virtual ICollection<Dossierresa> Dossierresa { get; set; }
        public virtual ICollection<Voyageur> Voyageur { get; set; }
    }
}
