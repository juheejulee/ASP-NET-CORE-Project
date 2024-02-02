using System.ComponentModel.DataAnnotations;

namespace ProjetFinSession.Models
{
    public class Utilisateur
    {
        [Key]
        public string IdUtilisateur { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Email { get; set; }



        public Utilisateur() { }
    }
}
