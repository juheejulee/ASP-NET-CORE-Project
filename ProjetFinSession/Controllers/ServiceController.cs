using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetFinSession.Data;
using ProjetFinSession.Models;

namespace ProjetFinSession.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Service>> Get()
        {
            var services = _context.Services.ToList();

            if (services == null)
            {
                return NotFound();
            }

            return Ok(new { services });
        }


        [HttpGet("{idService}", Name = "GetService")]
        public ActionResult<Service> Get(string idService)
        {
            var service = _context.Services.FirstOrDefault(item => item.IdService == idService);

            if (service == null)
            {
                return NotFound(new { error_message = $"Service {idService} not found!" });
            }

            return Ok(service);
        }

        [HttpPost]
        public ActionResult Post(Service service)
        {

            var findService = _context.Services.FirstOrDefault(item => item.IdService == service.IdService);

            if (findService != null)
            {
                return BadRequest(new { error_message = "This IdService already exists!" });

            }
            try
            {
                _context.Services.Add(service);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Something went wrong when trying to save!" });
            }


            return new CreatedAtRouteResult(
                "GetService",
                new { idService = service.IdService },
                service
                );
        }

        [HttpPut("{idService}")]
        public ActionResult Put(string idService, Service service)
        {
            if (idService != service.IdService)
            {
                return BadRequest(new { error_message = "Ids do not match!" });
            }

            var serviceToUpdate = _context.Services.FirstOrDefault(item => item.IdService == service.IdService);
            if (serviceToUpdate == null)
            {
                return NotFound(new { error_message = $"Service {idService} not found!" });
            }

            _context.Entry(serviceToUpdate).State = EntityState.Detached;


            try
            {
                _context.Services.Update(service);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = $"Something went wrong when trying to update! {ex}" });
            }


            return Ok(service);
        }

        [HttpDelete("{idService}")]
        public ActionResult Delete(string idService)
        {
            var service = _context.Services.FirstOrDefault(item => item.IdService == idService);

            if (service == null)
            {
                return NotFound(new { error_message = $"Service {idService} not found!" });
            }

            var commandes = _context.Commandes.Where(item => item.IdService == idService).ToList();


            try
            {
                _context.Remove(service);

                foreach (var commande in commandes)
                {
                    _context.Remove(commande);
                }

                _context.SaveChanges();

                return Ok(new { deleted_service = service, deleted_commandes = commandes });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error_message = "Something went wrong when trying to delete!" });
            }
        }
    }
}
