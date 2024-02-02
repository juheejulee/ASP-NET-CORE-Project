using System.ComponentModel.DataAnnotations;

namespace ProjetFinSession.Models
{
        public class Service
        {
            [Key]
            public string IdService { get; set; }
            [Required]
            public string NameService { get; set; }



            public Service() { }
        }
    }