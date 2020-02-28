using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoVoyageFinal.Models
{
    public partial class Personne : IValidatableObject
    {
        public Personne()
        {
            Voyageur = new HashSet<Voyageur>();
        }

        public int Id { get; set; }
        public byte TypePers { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez sélectionner une valeur.")]
        public string Civilite { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez saisir un Nom."), MinLength(1), StringLength(30)]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez saisir un Nom."), MinLength(1), StringLength(30)]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez saisir un email."), MinLength(1), StringLength(30)]
        public string Email { get; set; }
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez renseigner une date de naissance.")]
        [DataType(DataType.Date)]
        public DateTime? Datenaissance { get; set; }

        public virtual Client Client { get; set; }
        public virtual ICollection<Voyageur> Voyageur { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
