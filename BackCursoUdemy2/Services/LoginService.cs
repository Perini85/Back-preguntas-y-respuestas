using BackCursoUdemy2.Domain.IRepositories;
using BackCursoUdemy2.Domain.IServices;
using BackCursoUdemy2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackCursoUdemy2.Services
{
    public class LoginService : ILoginService
    {

        private readonly ILoginRepository _loginRepository;


        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }



        public async Task<Usuario> ValidateUser(Usuario usuario)
        {

            return await _loginRepository.ValidateUser(usuario);
        }

    }
}
