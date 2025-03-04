﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoVoyageFinal.Models
{
    public partial class Destination : IValidatableObject
    {
        public Destination()
        {
            InverseIdParenteNavigation = new HashSet<Destination>();
            Photo = new HashSet<Photo>();
            Voyage = new HashSet<Voyage>();
        }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez saisir un Id compris entre 0 et 99999"), Range(0, 99999)]
        [Display(Name = "Id")]
        public int Id { get; set; }

        public int? IdParente { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez renseigner un Nom de destination compris entre 1 et 30 caractères."), MinLength(1), StringLength(30)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Le genre doit comporter des lettres, tirets ou espaces, et commencer par une majuscule")]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez sélectionner un niveau compris en 1 et 3."), Range(0, 3)]
        [Display(Name = "Niveau")]
        public byte Niveau { get; set; }

        [Required(ErrorMessage = "Champ obligatoire. Veuillez renseigner une description comprise entre 5 et 1500 caractères."), MinLength(5), StringLength(1500)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Destination Parente")]
        public virtual Destination IdParenteNavigation { get; set; }
        public virtual ICollection<Destination> InverseIdParenteNavigation { get; set; }
        public virtual ICollection<Photo> Photo { get; set; }
        public virtual ICollection<Voyage> Voyage { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // On récupère le DbContext
            BoVoyageContext ctx = (BoVoyageContext)validationContext.GetService(typeof(BoVoyageContext));
            // On récupère l'aliment à valider
            Destination destination = validationContext.ObjectInstance as Destination;

            // On fait les contrôles
            // NB/ On utilise pas la vue locale, car le DbSet est instancié à chaque appel du contrôleur et donc pas chargé quand on arrive sur la vue de création d'un aliment

            var res =  ctx.Destination.Find( destination.Id);
            if (res != null)
            {
                yield return new ValidationResult("L'Id de Destination éxiste déjà", new string[] { "Id" });
            }

            res =  ctx.Destination.FirstOrDefault(d => d.Nom == destination.Nom);
            if (res != null)
            {
                yield return new ValidationResult("Ce nom de Destination éxiste déjà", new string[] { "Nom" });
            }
        }
    }
   
}
