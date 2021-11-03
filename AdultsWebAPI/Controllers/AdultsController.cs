using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdultsWebAPI.Data;
using AdultsWebAPI.Models;
using Microsoft.AspNetCore.Mvc;


namespace AdultsWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdultsController:ControllerBase
    {
        private IAdultsService adultsService;

        public AdultsController(IAdultsService adultsService)
        {
            this.adultsService = adultsService;
        }
        
         [HttpGet]
        public async Task<ActionResult<IList<Adult>>> GetAdults([FromQuery] int? id, [FromQuery] string? name)
        {
            try
            {
                IList<Adult> adults = await adultsService.GetAdultsAsync();
                if (id != null)
                {
                    adults = adults.Where(adult => adult.Id == id).ToList();
                }

                if (name != null)
                {
                    adults = adults.Where(adult => adult.LastName.Contains(name)).ToList();
                }
                return Ok(adults);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        

        [HttpPost]
        public async Task<ActionResult<Adult>> AddAdult([FromBody] Adult adult)
        {
            //check the model validation for adding
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Adult added = await adultsService.AddAdultAsync(adult);
                return Created($"/{added.Id}", added);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteAdult([FromRoute] int id)
        {
            try
            {
                await adultsService.RemoveAdultAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult<Adult>> UpdateAdult([FromBody] Adult adult)
        {
            try
            {
                Adult updatedAdult = await adultsService.EditAsync(adult);
                return Ok(updatedAdult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Adult>> GetAdultById([FromRoute] int id)
        {
            try
            {
                Adult adult = await adultsService.GetAsync(id);
               return Ok(adult);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
    
}