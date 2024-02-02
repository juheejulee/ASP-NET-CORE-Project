using System.ComponentModel.DataAnnotations;

namespace ProjetFinSession.Models
{
   
        public class Commande
        {
            [Key]
            public string IdCommande { get; set; }
            [Required]
            public string IdUtilisateur { get; set; }
            [Required]
            public string IdService { get; set; }
            [Required]
            public string Description { get; set; }
           

            public Commande() { }
        }
    }


