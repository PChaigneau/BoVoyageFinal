using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace BoVoyageFinal.Models
{
    public class EditRoleVm
    {

        public EditRoleVm()
        {
            Users = new List<string>(); //Pour ne pas avoir d'exception de type Value can not be null, car dans la vue nous utiliserons model.Users.any()
        }

        public string Id { get; set; }

        [Required(ErrorMessage = "Le nom du role est obligatoire")]
        public string RoleName { get; set; }

        public List<string> Users { get; set; }
    }
}

