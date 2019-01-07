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
    public class SociosController : ControllerBase
    {
        private readonly IEurovalCmsService _serviceCms;
        private readonly ILogger<SociosController> _logger;

        public SociosController(IEurovalCmsService context, ILogger<SociosController> logging)
        {
            _serviceCms = context;
            this._logger = logging;
        }

        // GET: api/Socios
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<SocioViewModel>>> GetSocios()
        {
            try
            {
                return  Ok(await _serviceCms.GetSociosAsync());

            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to get Pisas: {ex}");
                return BadRequest("Failed to get Socioss");
            }
        }

        // GET: api/Socios/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<SocioViewModel>> GetSocio([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var socio = await _serviceCms.GetSocioAsync(id);

                if (socio == null)
                {
                    return NotFound($"Socio {id} not found");
                }

                return Ok(socio);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Socio: {ex}");
                return BadRequest("Failed to get Socio");
            }
        }

        // PUT: api/Socios/5
        [HttpPut("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<SocioViewModel>> PutSocio([FromRoute] int id, [FromBody] SocioViewModel socio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != socio.Id)
            {
                return BadRequest("Id from route is different from entity");
            }

            try
            {
                socio = await _serviceCms.UpdateSocioAsync(socio);
                if(socio == null)
                {
                    return NotFound($"Socio {id} Not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Update Socio: {ex}");
                return BadRequest($"Failed to update socio {socio.Id}");
            }

            return NoContent();
        }

        // POST: api/Socios
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<SocioViewModel>> PostSocio([FromBody] SocioViewModel socio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
               await _serviceCms.CreateSocioAsync(socio);
              
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create Socio: {ex}");
                return BadRequest("Failed Create Socio");
            }

            return CreatedAtAction("GetSocio", new { id = socio.Id }, socio);
        }

        // DELETE: api/Socios/5
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Socio>> DeleteSocio([FromRoute] int id)
        {
            SocioViewModel socio;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                socio = await _serviceCms.GetSocioAsync(id);
                if (socio == null)
                {
                    return NotFound($"Socio is not found {id}");
                }

                await _serviceCms.RemoveSocioAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create Socio: {ex}");
                return BadRequest($"Failed to delete Socio");
            }

            return Ok(socio);
        }

        private async Task<bool> SocioExists(int id)
        {
            return await _serviceCms.SocioExistsAsync(id);
        }
    }
}