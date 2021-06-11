using BackCursoUdemy2.Domain.IServices;
using BackCursoUdemy2.Domain.Models;
using BackCursoUdemy2.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackCursoUdemy2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RespuestaCuestionarioController : ControllerBase
    {

        private readonly IRespuestaCuestionarioService _respuestaCuestionarioService;

        private readonly ICuestionarioService _cuestionarioService;

        public RespuestaCuestionarioController(IRespuestaCuestionarioService respuestaCuestionarioService, ICuestionarioService cuestionarioService)
        {
            _respuestaCuestionarioService = respuestaCuestionarioService;

            _cuestionarioService = cuestionarioService;
        }



        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RespuestaCuestionario respuestaCuestionario)
        {
            try
            {

                await _respuestaCuestionarioService.SaveRespuestaCuestionario(respuestaCuestionario);

                return Ok(new { message = " Se agrego la respuesta al cuestionario exitosamente" });

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{idCuestionario}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult>  Get(int idCuestionario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);
                var listRespuestaCuestionario = await _respuestaCuestionarioService.ListRespuestaCuestionario(idCuestionario, idUsuario);

                  if(listRespuestaCuestionario == null)
                {
                    return BadRequest(new { message = "Error al buscar listado de respuestas" });
                }

                return Ok(listRespuestaCuestionario);
               
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);

                var respuestaCuestionario = await _respuestaCuestionarioService.BuscarRespuestaCuestionario(id, idUsuario);
                if(respuestaCuestionario == null)
                {
                    return BadRequest(new { message = " Error al buscar la respuesta al cuestionario" });
                }

                await _respuestaCuestionarioService.EliminarRespuestaCuestionario(respuestaCuestionario);

                return Ok(new { message = "la respuesta fue eliminada con exito" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetCuestionarioByIdRespuesta/{idRespuesta}")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult>  GetCuestionarioByIdRespuesta(int idRespuesta)
        {
            try
            {
                //Obtener el idCuestionaro dado un idRespuesta
                int idCuestionario = await _respuestaCuestionarioService.GetIdCuestionarioByIdRespuesta(idRespuesta);

                //Buscar el cuestionario
                var cuestionario = await _cuestionarioService.GetCuestionario(idCuestionario);



                //Buscar las respuesta seleccionadas dado un idRespuesta
                var listRespuestas = await _respuestaCuestionarioService.GetListRespuestas(idRespuesta);
                return Ok(new { cuestionario = cuestionario, respuestas = listRespuestas});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
