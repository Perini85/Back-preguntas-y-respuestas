using BackCursoUdemy2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackCursoUdemy2.Domain.IServices
{
   public interface ICuestionarioService
    {

       Task CreateCuestionario(Cuestionario cuestionario);

        Task<List<Cuestionario>> GetCuestionarioByUser(int idUsuario);
        Task<Cuestionario> GetCuestionario(int idCuestionario);

        Task<Cuestionario> BuscarCuestionario(int idCuestionario, int idUsuario);

        Task EliminarCuestionario(Cuestionario cuestionario);

        Task<List<Cuestionario>> GetListCuestionarios();



    }
}
