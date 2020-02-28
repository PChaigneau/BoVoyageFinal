using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoVoyageFinal.Models
{
    public partial class Destination
    {
        public Destination()
        {
            InverseIdParenteNavigation = new HashSet<Destination>();
            Photo = new HashSet<Photo>();
            Voyage = new HashSet<Voyage>();
        }

        [Required(ErrorMessage = "Champ obligatoire."), Range(0, 99999)]
        public int Id { get; set; }
        
        public int? IdParente { get; set; }
        [Required(ErrorMessage = "Champ obligatoire. Veuillez renseigner un Nom de destination compris entre 1 et 30 caractères."), MinLength(1), StringLength(30)]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Champ obligatoire. Veuillez sélectionner un niveau compris en 1 et 3"), Range(0, 99999)]
        public byte Niveau { get; set; }
        [Required(ErrorMessage = "Champ obligatoire. Veuillez renseigner une description comprise entre 20 et 1500 caractères"), MinLength(20), StringLength(1500)]
        public string Description { get; set; }

        public virtual Destination IdParenteNavigation { get; set; }
        public virtual ICollection<Destination> InverseIdParenteNavigation { get; set; }
        public virtual ICollection<Photo> Photo { get; set; }
        public virtual ICollection<Voyage> Voyage { get; set; }
    }

   
}
