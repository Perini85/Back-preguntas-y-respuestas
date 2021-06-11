using BackCursoUdemy2.Domain.IRepositories;
using BackCursoUdemy2.Domain.Models;
using BackCursoUdemy2.Persistence.Contex;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackCursoUdemy2.Persistence.Repositories
{
    public class RespuestaCuestionarioRepository: IRespuestaCuestionarioRepository
    {

        private readonly AplicationDbContext _context;

        public RespuestaCuestionarioRepository( AplicationDbContext context)
        {
            _context = context;
        }

       
        public async Task SaveRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {
            respuestaCuestionario.Activo = 1;
            respuestaCuestionario.Fecha = DateTime.Now;
            _context.Add(respuestaCuestionario);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RespuestaCuestionario>> ListRespuestaCuestionario(int idCuestionario, int idUsuario)
        {
            var ListRespuestaCuestionario = await _context.RespuestaCuestionario.Where(r => r.CuestionarioId == idCuestionario
                                                                              && r.Activo == 1 && r.Cuestionario.UsuarioId == idUsuario)
                                                                             .OrderByDescending(r => r.Fecha).ToListAsync();

            return ListRespuestaCuestionario;
        }

      
        public async Task<RespuestaCuestionario> BuscarRespuestaCuestionario(int idRtaCuestionario, int idUsuario)
        {
            var rtaCuestionario = await _context.RespuestaCuestionario.Where(r => r.Id == idRtaCuestionario &&
                                                                             r.Activo == 1 &&
                                                                             r.Cuestionario.UsuarioId == idUsuario).FirstOrDefaultAsync();

            return rtaCuestionario;
        }


        public async Task EliminarRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {

            respuestaCuestionario.Activo = 0;
            _context.Entry(respuestaCuestionario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetIdCuestionarioByIdRespuesta(int idRespuesta)
        {
            var cuestionario = await _context.RespuestaCuestionario.Where(r => r.Id == idRespuesta &&
                                                                            r.Activo == 1).FirstOrDefaultAsync();

            return cuestionario.CuestionarioId;
        }


        public async Task<List<RespuestaCuestionarioDetalle>>  GetListRespuestas(int idRespuestaCuestionario)
        {

            var listRespuesta = await _context.RespuestaCuestionarioDetalles.Where(r => r.RespuestaCuestionarioId == idRespuestaCuestionario)
                                                                                             .Select(r => new RespuestaCuestionarioDetalle
                                                                                             {

                                                                                                 RespuestaId = r.RespuestaId
                                                                                             }).ToListAsync();

            return listRespuesta;
        }
    }
}
