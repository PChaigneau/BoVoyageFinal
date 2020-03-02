using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BoVoyageFinal.Models
{
    public partial class Personne : IValidatableObject
    {
        public Personne()
        {
            Voyageur = new HashSet<Voyageur>();
        }

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez sélectionner une valeur comprise entre 1 et 2."), Range(1, 2)]
        [Display(Name = "Type de Personne")]
        public byte TypePers { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez sélectionner une valeur.")]
        [Display(Name = "Civilite")]
        public string Civilite { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez saisir un Nom."), MinLength(1), StringLength(30)]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez saisir un Nom."), MinLength(1), StringLength(30)]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez saisir un email."), MinLength(1), StringLength(30)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez saisir un numéro de téléphone.")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Téléphone")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez renseigner une date de naissance.")]
        [DataType(DataType.Date)]
        [Display(Name = "Date de naissance")]
        public DateTime? Datenaissance { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<Voyageur> Voyageur { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // On récupère le DbContext
            BoVoyageContext ctx = (BoVoyageContext)validationContext.GetService(typeof(BoVoyageContext));
            // On récupère l'aliment à valider
            Personne personne = validationContext.ObjectInstance as Personne;

            // On fait les contrôles
            // NB/ On utilise pas la vue locale, car le DbSet est instancié à chaque appel du contrôleur et donc pas chargé quand on arrive sur la vue de création d'un aliment

            var res = ctx.Destination.Find(personne.Id);
            if (res != null)
            {
                yield return new ValidationResult("Cet Id de Personne éxiste déjà", new string[] { "Id" });
            }

            var nom = ctx.Destination.FirstOrDefault(d => d.Nom == personne.Nom);
            if (nom != null)
            {
                yield return new ValidationResult("Ce nom de Personne éxiste déjà", new string[] { "Nom" });
            }
        }
    }
}
