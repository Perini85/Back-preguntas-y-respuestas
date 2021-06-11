﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackCursoUdemy2.Domain.IServices;
using BackCursoUdemy2.Domain.Models;
using BackCursoUdemy2.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackCursoUdemy2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuestionarioController : ControllerBase
    {

        private readonly ICuestionarioService _cuestionarioService;


        public CuestionarioController(ICuestionarioService cuestionarioService)
        {

            _cuestionarioService = cuestionarioService;
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Post([FromBody]Cuestionario cuestionario)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);

                cuestionario.UsuarioId = idUsuario;
                cuestionario.Activo = 1;
                cuestionario.FechaCreacion = DateTime.Now;
                await _cuestionarioService.CreateCuestionario(cuestionario);

                return Ok(new { message = " Se agrego el cuestionario exitosamente" });

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }

        [Route("GetListCuestionarioByUser")]
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetCuestionarioByUser()
        {

            try
            {

                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);

                var listCuestionario = await _cuestionarioService.GetCuestionarioByUser(idUsuario);

                return Ok(listCuestionario);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);

            }


        }

        [HttpGet("{idCuestionario}")]
        public async Task<IActionResult>  Get(int idCuestionario)
        {
            try
            {

                var cuestionario = await _cuestionarioService.GetCuestionario(idCuestionario);
                return Ok(cuestionario);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }



        }

        [HttpDelete("{idCuestionario}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int idCuestionario)
        {

            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);

                var cuestionario = await _cuestionarioService.BuscarCuestionario(idCuestionario, idUsuario);
                if (cuestionario == null)
                {

                    return BadRequest(new { message = "No se encontro ningun cuestionario" });
                }

                await _cuestionarioService.EliminarCuestionario(cuestionario);
                return Ok(new { message = "el cuestionario fue eliminadocon exito" });

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Route("GetListCuestionarios")]
        [HttpGet]
        public async Task<IActionResult> GetListCuestionarios()
        {

            try
            {

                var ListCuestionarios = await _cuestionarioService.GetListCuestionarios();
                return Ok(ListCuestionarios);


            }catch(Exception ex)
            {

                return BadRequest(ex.Message);

            }

        }


    }
}
