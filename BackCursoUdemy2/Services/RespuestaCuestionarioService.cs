using BackCursoUdemy2.Domain.IRepositories;
using BackCursoUdemy2.Domain.IServices;
using BackCursoUdemy2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackCursoUdemy2.Services
{
    public class RespuestaCuestionarioService: IRespuestaCuestionarioService

    {

        private readonly IRespuestaCuestionarioRepository _respuestaCuestionarioRepository;

        public RespuestaCuestionarioService ( IRespuestaCuestionarioRepository respuestaCuestionarioRepository)
        {
            _respuestaCuestionarioRepository = respuestaCuestionarioRepository;
        }

       
        public async Task SaveRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {
             await  _respuestaCuestionarioRepository.SaveRespuestaCuestionario(respuestaCuestionario);
        }

        public async Task<List<RespuestaCuestionario>> ListRespuestaCuestionario(int idCuestionario, int idUsuario)
        {
            return await _respuestaCuestionarioRepository.ListRespuestaCuestionario(idCuestionario, idUsuario);
        }

        public async Task<RespuestaCuestionario> BuscarRespuestaCuestionario(int idRtaCuestionario,int idUsuario)
        {
            return await _respuestaCuestionarioRepository.BuscarRespuestaCuestionario(idRtaCuestionario, idUsuario);
        }

        public async Task EliminarRespuestaCuestionario(RespuestaCuestionario respuestaCuestionario)
        {
            await _respuestaCuestionarioRepository.EliminarRespuestaCuestionario(respuestaCuestionario);
        }


        public async Task<int> GetIdCuestionarioByIdRespuesta(int idRespuestaCuestionario)
        {
            return await _respuestaCuestionarioRepository.GetIdCuestionarioByIdRespuesta(idRespuestaCuestionario);
        }

        public async Task<List<RespuestaCuestionarioDetalle>> GetListRespuestas(int idRespuestaCuestionario)
        {
            return await _respuestaCuestionarioRepository.GetListRespuestas(idRespuestaCuestionario);
        }
    }
}
