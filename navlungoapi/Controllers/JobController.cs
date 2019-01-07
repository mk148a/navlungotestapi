using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using navlungoapi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace navlungoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly JobContext _context;
        public JobController(JobContext context)
        {
            _context = context;

            if (_context.Jobs.Count() == 0)
            {

                _context.Jobs.Add(new Job { shipper = "navlungo", destinationCountry = "Almanya",  mailadress = "mk148a@hotmail.com", schedule = "* * * * *" } );
                // _context.Jobs.Add(new Job { shipper = "navlungo", destinationCountry = "Almanya"});
                _context.SaveChanges();

            }
        }

       

        

        #region snippet_GetAll
        //Get api/jobs ile çağırılır
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            return await _context.Jobs.ToListAsync();
        }

        #region snippet_GetByID        
        //Get api/job/navlungo shipper adı ile çağırılır
        [HttpGet("{shipper}")]
        public async Task<ActionResult<Job>> GetJob(string shipper)
        {
            var job = await _context.Jobs.FindAsync(shipper);
            if (job == null)
            {
                return NotFound();
            }

            return job;
        }
        #endregion
        #endregion

        #region snippet_Create
        // POST: api/Job
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJob", new { shipper = job.shipper }, job);
        }
        #endregion

        #region snippet_Update
        // PUT: api/Job/5
        [HttpPut("{shipper}")]
        public async Task<IActionResult> PutJob(string shipper, Job Job)
        {
            if (shipper != Job.shipper)
            {
                return BadRequest();
            }

            _context.Entry(Job).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region snippet_Delete
        // DELETE: api/Job/5
        [HttpDelete("{shipper}")]
        public async Task<ActionResult<Job>> DeleteJob(string shipper)
        {
            var Job = await _context.Jobs.FindAsync(shipper);
            if (Job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(Job);
            await _context.SaveChangesAsync();

            return Job;
        }
        #endregion
    }

}
