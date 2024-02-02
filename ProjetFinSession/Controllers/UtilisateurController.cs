using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetFinSession.Data;
using ProjetFinSession.Models;

namespace ProjetFinSession.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UtilisateurController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Utilisateur>> Get()
        {
            var utilisateurs = _context.Utilisateurs.ToList();

            if (utilisateurs == null)
            {
                return NotFound();
            }

            return Ok(new { utilisateurs });
        }


        [HttpGet("{idUtilisateur}", Name = "GetUtilisateur")]
        public ActionResult<Utilisateur> Get(string idUtilisateur)
        {
            var utilisateur = _context.Utilisateurs.FirstOrDefault(item => item.IdUtilisateur == idUtilisateur);

            if (utilisateur == null)
            {
                return NotFound(new { error_message = $"Service {idUtilisateur} not found!" });
            }

            return Ok(utilisateur);
        }

        [HttpPost]
        public ActionResult Post(Utilisateur utilisateur)
        {

            var findUtilisateur = _context.Utilisateurs.FirstOrDefault(item => item.IdUtilisateur == utilisateur.IdUtilisateur);

            if (findUtilisateur != null)
            {
                return BadRequest(new { error_message = "This IdUtilisateur already exists!" });

            }
            try
            {
                _context.Utilisateurs.Add(utilisateur);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Something went wrong when trying to save!" });
            }


            return new CreatedAtRouteResult(
                "GetUtilisateur",
                new { idUtilisateur = utilisateur.IdUtilisateur },
                utilisateur
                );
        }

        [HttpPut("{idUtilisateur}")]
        public ActionResult Put(string idUtilisateur, Utilisateur utilisateur)
        {
            if (idUtilisateur != utilisateur.IdUtilisateur)
            {
                return BadRequest(new { error_message = "Ids do not match!" });
            }

            var utilisateurToUpdate = _context.Utilisateurs.FirstOrDefault(item => item.IdUtilisateur == utilisateur.IdUtilisateur);
            if (utilisateurToUpdate == null)
            {
                return NotFound(new { error_message = $"Service {idUtilisateur} not found!" });
            }

            _context.Entry(utilisateurToUpdate).State = EntityState.Detached;


            try
            {
                _context.Utilisateurs.Update(utilisateur);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = $"Something went wrong when trying to update! {ex}" });
            }


            return Ok(utilisateur);
        }

        [HttpDelete("{idUtilisateur}")]
        public ActionResult Delete(string idUtilisateur)
        {
            var utilisateur = _context.Utilisateurs.FirstOrDefault(item => item.IdUtilisateur == idUtilisateur);

            if (utilisateur == null)
            {
                return NotFound(new { error_message = $"Service {idUtilisateur} not found!" });
            }

            var commandes = _context.Commandes.Where(item => item.IdUtilisateur == idUtilisateur).ToList();


            try
            {
                _context.Remove(utilisateur);

                foreach (var commande in commandes)
                {
                    _context.Remove(commande);
                }

                _context.SaveChanges();

                return Ok(new { deleted_utilisateur = utilisateur, deleted_commandes = commandes });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Something went wrong when trying to delete!" });
            }
        }
    }
}
