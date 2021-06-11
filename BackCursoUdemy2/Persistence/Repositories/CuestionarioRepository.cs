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
    public class CuestionarioRepository: ICuestionarioRepository
    {

        private readonly AplicationDbContext _context;


        public CuestionarioRepository(AplicationDbContext context)
        {

            _context = context;
        }


        public async Task CreateCuestionario(Cuestionario cuestionario)
        {

            _context.Add(cuestionario);

            await _context.SaveChangesAsync();

        }

      

        public  async Task<List<Cuestionario>> GetCuestionarioByUser(int idUsuario)
        {
          var listCuestionario = await _context.Cuestionario.Where(c => c.Activo == 1 
                                            && c.UsuarioId == idUsuario).ToListAsync();

                          return listCuestionario;
        }

        public async Task<Cuestionario> GetCuestionario(int idCuestionario)
        {
        var cuestionario = await _context.Cuestionario.Where(c => c.Id == idCuestionario
                                                    && c.Activo == 1)
                                                   .Include(x =>x.listPreguntas)
                                                   .ThenInclude(x =>x.listRespuesta)
                                                    .FirstOrDefaultAsync();
            return cuestionario;
        }


        public async Task<Cuestionario> BuscarCuestionario(int idCuestionario, int idUsuario)
        {

            var cuestionario = await _context.Cuestionario.Where(c => c.Id == idCuestionario
                                                                 && c.Activo == 1  &&
                                                                 c.UsuarioId == idUsuario).FirstOrDefaultAsync();
            return cuestionario;
        }

        public async Task EliminarCuestionario(Cuestionario cuestionario)
        {

            cuestionario.Activo = 0;
            _context.Entry(cuestionario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cuestionario>> GetListCuestionarios()
        {

            var listCuestionarios = await _context.Cuestionario.Where(c => c.Activo == 1)
                                                                 .Select(c => new Cuestionario
                                                                 {
                                                                     Id = c.Id,
                                                                     Nombre = c.Nombre,
                                                                     Descripcion = c.Descripcion,
                                                                     FechaCreacion = c.FechaCreacion,
                                                                     Usuario = new Usuario
                                                                     {
                                                                         nombreUsuario = c.Usuario.nombreUsuario
                                                                     }


                                                                 }).ToListAsync();

            return listCuestionarios;


        }

    }
}
