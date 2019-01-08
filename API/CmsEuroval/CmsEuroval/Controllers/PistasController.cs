using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EurovalDataAccess;
using EurovalDataAccess.Entities;
using Microsoft.Extensions.Logging;
using EurovalBusinessLogic.Services;
using EurovalBusinessLogic.Services.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CmsEuroval
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles ="admin")]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    [Route("api/[controller]")]
    [ApiController]
    [Produces("text/json")]
    public class PistasController : ControllerBase
    {
        private readonly IEurovalCmsService _serviceCms;
        private readonly ILogger<PistasController> _logger;

        public PistasController(IEurovalCmsService context, ILogger<PistasController> logging)
        {
            _serviceCms = context;
            this._logger = logging;
        }

        // GET: api/Pistas
        /// <summary>
        /// Get all pistas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<PistaViewModel>>> GetPistas()
        {
            try
            {
                return  Ok(await _serviceCms.GetPistasAsync());

            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to get Pisas: {ex}");
                return BadRequest("Failed to get pistass");
            }
        }

        // GET: api/Pistas/5
        /// <summary>
        /// Get the pista with the id passed as parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PistaViewModel>> GetPista([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var pista = await _serviceCms.GetPistaAsync(id);

                if (pista == null)
                {
                    return NotFound($"Pista {id} not found");
                }

                return Ok(pista);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Pista: {ex}");
                return BadRequest("Failed to get Pista");
            }
        }

        // PUT: api/Pistas/5
        /// <summary>
        /// Updates the pista passed as a parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pista"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PistaViewModel>> PutPista([FromRoute] int id, [FromBody] PistaViewModel pista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pista.Id)
            {
                return BadRequest("Id from route is different from entity");
            }

            try
            {
                pista = await _serviceCms.UpdatePistaAsync(pista);
                if(pista == null)
                {
                    return NotFound($"Pista {id} Not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Update Pista: {ex}");
                return BadRequest($"Failed to update pista {pista.Id}");
            }

            return NoContent();
        }

        // POST: api/Pistas
        /// <summary>
        /// Creates a new pista.
        /// </summary>
        /// <param name="pista"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<PistaViewModel>> PostPista([FromBody] PistaViewModel pista)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
               await _serviceCms.CreatePistaAsync(pista);
              
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create Pista: {ex}");
                return BadRequest("Failed Create Pista");
            }

            return CreatedAtAction("GetPista", new { id = pista.Id }, pista);
        }

        // DELETE: api/Pistas/5
        /// <summary>
        /// Deletes the pista with the id passed as parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PistaViewModel>> DeletePista([FromRoute] int id)
        {
            PistaViewModel pista;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                pista = await _serviceCms.GetPistaAsync(id);
                if (pista == null)
                {
                    return NotFound($"Pista is not found {id}");
                }

                await _serviceCms.RemovePistaAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create Pista: {ex}");
                return BadRequest($"Failed to delete pista");
            }

            return Ok(pista);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<bool> PistaExists(int id)
        {
            return await _serviceCms.PistaExistsAsync(id);
        }
    }
}