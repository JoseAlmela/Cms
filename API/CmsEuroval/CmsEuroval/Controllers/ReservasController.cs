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
    public class ReservasController : ControllerBase
    {
        private readonly IEurovalCmsService _serviceCms;
        private readonly ILogger<ReservasController> _logger;

        public ReservasController(IEurovalCmsService context, ILogger<ReservasController> logging)
        {
            _serviceCms = context;
            this._logger = logging;
        }

        // GET: api/Reservas
        /// <summary>
        /// get all reservas
        /// </summary>
        /// <param name="includeExtraInfo"> if true it also get all the relationships.</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<ReservaViewModel>>> GetReservas(bool includeExtraInfo = true)
        {
            try
            {
                return  Ok(await _serviceCms.GetReservasAsync(includeExtraInfo));

            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to get Pisas: {ex}");
                return BadRequest("Failed to get Reservass");
            }
        }

        // GET: api/Reservas/5
        /// <summary>
        /// Get the reserva with the id passed as parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ReservaViewModel>> GetReserva([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var reserva = await _serviceCms.GetReservaAsync(id);

                if (reserva == null)
                {
                    return NotFound($"Reserva {id} not found");
                }

                return Ok(reserva);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Reserva: {ex}");
                return BadRequest("Failed to get Reserva");
            }
        }

        // PUT: api/Reservas/5
        /// <summary>
        /// Updates the reserva passed as a parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="reserva"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ReservaViewModel>> PutReserva([FromRoute] int id, [FromBody] ReservaViewModel reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reserva.Id)
            {
                return BadRequest("Id from route is different from entity");
            }

            try
            {
                reserva = await _serviceCms.UpdateReservaAsync(reserva);
                if(reserva == null)
                {
                    return NotFound($"Reserva {id} Not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to Update Reserva: {ex}");
                return BadRequest($"Failed to update reserva {reserva.Id}");
            }

            return NoContent();
        }

        // POST: api/Reservas
        /// <summary>
        /// Creates a new reserva.
        /// </summary>
        /// <param name="reserva"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ReservaViewModel>> PostReserva([FromBody] ReservaViewModel reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                reserva =  await _serviceCms.CreateReservaAsync(reserva);
                if(reserva == null)
                {
                    return BadRequest("reserva not created");
                }


            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create Reserva: {ex}");
                return BadRequest("Failed Create Reserva");
            }

            return CreatedAtAction("GetReserva", new { id = reserva.Id }, reserva);
        }

        // DELETE: api/Reservas/5
        /// <summary>
        /// Deletes the pista with the id passed as parameter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Reserva>> DeleteReserva([FromRoute] int id)
        {
            ReservaViewModel reserva;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                reserva = await _serviceCms.GetReservaAsync(id);
                if (reserva == null)
                {
                    return NotFound($"Reserva is not found {id}");
                }

                await _serviceCms.RemoveReservaAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to create Reserva: {ex}");
                return BadRequest($"Failed to delete Reserva");
            }

            return Ok(reserva);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<bool> ReservaExists(int id)
        {
            return await _serviceCms.ReservaExistsAsync(id);
        }
    }
}