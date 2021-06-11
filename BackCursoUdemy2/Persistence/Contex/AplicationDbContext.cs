using BackCursoUdemy2.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackCursoUdemy2.Persistence.Contex
{
    public class AplicationDbContext: DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pregunta> Pregunta { get; set; }
        public DbSet<Cuestionario> Cuestionario { get; set; }
        public DbSet<Respuesta> Respuesta { get; set; }

        public DbSet<RespuestaCuestionario> RespuestaCuestionario { get; set; }

        public DbSet<RespuestaCuestionarioDetalle> RespuestaCuestionarioDetalles { get; set; }



        public AplicationDbContext(DbContextOptions<AplicationDbContext>options): base(options)
        {

        }


    }
}
