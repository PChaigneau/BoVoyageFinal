using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BoVoyageFinal.Models
{
    public partial class Voyage : IValidatableObject
    {
        public Voyage()
        {
            Dossierresa = new HashSet<Dossierresa>();
            Voyageur = new HashSet<Voyageur>();
        }

        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez sélectionner une Destination.")]
        [Display(Name = "Destination")]
        public int IdDestination { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez sélectionner une Date de départ.")]
        [Display(Name = "Date de départ")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateDepart { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez sélectionner une Date de retour.")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date de retour")]
        [DataType(DataType.Date)]
        public DateTime DateRetour { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez indiquer le nombre de places disponiles.")]
        [Display(Name = "Places disponibles")]
        public int PlacesDispo { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez saisir un PrixHT."), Range(0, 99999)]
        [DataType(DataType.Currency)]
        [Display(Name = "PrixHt")]
        public decimal PrixHt { get; set; }
        
        [Required(ErrorMessage = "Champ obligatoire. Veuillez saisir une valeur entre 0 et 1."), Range(0, 1)]
        [Display(Name = "Réduction")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public decimal Reduction { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez renseigner une description comprise entre 20 et 1500 caractères."), StringLength(1500, MinimumLength=5)]
        [Display(Name = "Descriptif")]
        public string Descriptif { get; set; }

        [Display(Name = "Destination")]
        public virtual Destination IdDestinationNavigation { get; set; }
        public virtual ICollection<Dossierresa> Dossierresa { get; set; }
        public virtual ICollection<Voyageur> Voyageur { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // On récupère le DbContext
            BoVoyageContext ctx = (BoVoyageContext)validationContext.GetService(typeof(BoVoyageContext));
            // On récupère le voyage à valider
            Voyage voyage = validationContext.ObjectInstance as Voyage;

            // On fait les contrôles
            // NB/ On utilise pas la vue locale, car le DbSet est instancié à chaque appel du contrôleur et donc pas chargé quand on arrive sur la vue de création d'un aliment

            if (voyage.DateDepart >= voyage.DateRetour)
            {
                yield return new ValidationResult("La date de départ doit être inférieure à la date de retour", new string[] { "DateDepart", "DateRetour" });
            }
        }
    }
}
