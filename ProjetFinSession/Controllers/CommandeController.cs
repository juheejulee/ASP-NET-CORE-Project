using Microsoft.AspNetCore.Mvc;
using ProjetFinSession.Data;
using ProjetFinSession.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ProjetFinSession.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommandeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Commande>> Get()
        {
            var commandes = _context.Commandes.ToList();

            if (commandes == null)
            {
                return NotFound();
            }

            return Ok(new { commandes });
        }


        [HttpGet("{idCommande}", Name = "GetCommande")]
        public ActionResult<Commande> Get(string idCommande)
        {
            var commande = _context.Commandes.FirstOrDefault(item => item.IdCommande == idCommande);

            if (commande == null)
            {
                return NotFound(new { error_message = $"Commande {idCommande} not found!" });
            }

            return Ok(commande);
        }

        [HttpPost]
        public ActionResult Post(Commande commande)
        {

            var findCommande = _context.Commandes.FirstOrDefault(item => item.IdCommande == commande.IdCommande);

            if (findCommande != null)
            {
                return BadRequest(new { error_message = "This IdCommande already exists!" });

            }
            try
            {
                _context.Commandes.Add(commande);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Something went wrong when trying to save!" });
            }


            return new CreatedAtRouteResult(
                "GetCommande",
                new { idCommande = commande.IdCommande },
                commande
                );
        }

        [HttpPut("{idCommande}")]
        public ActionResult Put(string idCommande, Commande commande)
        {
            if (idCommande != commande.IdCommande)
            {
                return BadRequest(new { error_message = "Ids do not match!" });
            }

            var commandeToUpdate = _context.Commandes.FirstOrDefault(item => item.IdCommande == commande.IdCommande);
            if (commandeToUpdate == null)
            {
                return NotFound(new { error_message = $"Commande {idCommande} not found!" });
            }

            _context.Entry(commandeToUpdate).State = EntityState.Detached;


            try
            {
                _context.Commandes.Update(commande);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = $"Something went wrong when trying to update! {ex}" });
            }


            return Ok(commande);
        }

        [HttpDelete("{idCommande}")]
        public ActionResult Delete(string idCommande)
        {
            var commande = _context.Commandes.FirstOrDefault(item => item.IdCommande == idCommande);

            if (commande == null)
            {
                return NotFound(new { error_message = $"Commande {idCommande} not found!" });
            }

            try
            {
                _context.Remove(commande);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Something went wrong when trying to delete!" });
            }

            return Ok(new { deleted_commande = commande });
        }
    }
}