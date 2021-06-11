using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackCursoUdemy2.Domain.IServices;
using BackCursoUdemy2.Domain.Models;
using BackCursoUdemy2.DTO;
using BackCursoUdemy2.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackCursoUdemy2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {

            _usuarioService = usuarioService;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Usuario usuario)
        {

            try
            {

                var validateExistence =  await _usuarioService.ValidateExistence(usuario);
                if (validateExistence)
                {
                    return BadRequest(new { message = "El usuario " + usuario.nombreUsuario + " Ya existe" });
                }

                usuario.Password = Encriptar.EncriptarPassword(usuario.Password);
                await _usuarioService.SaveUser(usuario);


                return Ok(new { message = "Usuario registrado con exito" });

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [Route("CambiarPassword")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public async Task<IActionResult> CambiarPassword([FromBody] CambiarPasswordDTO cambiarPassword)
        {

            try
            {
               var identity = HttpContext.User.Identity as ClaimsIdentity;
                int idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);
                string passwordEncriptado = Encriptar.EncriptarPassword(cambiarPassword.passwordAnterior);
                var usuario = await _usuarioService.ValidatePassword(idUsuario, passwordEncriptado);

                if( usuario == null)
                {
                    return BadRequest(new { message = "la password es incorrecta" });
                } else
                {
                    usuario.Password = Encriptar.EncriptarPassword(cambiarPassword.nuevaPassword);
                    await  _usuarioService.UpdatePassword(usuario);
                    return Ok(new { message = "la password fue actualizada con exito" });
                }
               

            }catch(Exception )
            {

                return BadRequest();
            }

        }
    }
}
