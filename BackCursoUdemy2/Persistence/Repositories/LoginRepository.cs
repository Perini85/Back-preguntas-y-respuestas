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
    public class LoginRepository : ILoginRepository
    {


        private readonly AplicationDbContext _context;


        public LoginRepository(AplicationDbContext context)
        {

            _context = context;
        }

        public async Task<Usuario> ValidateUser(Usuario usuario)
        {

            var user =  await _context.Usuarios.Where(u => u.nombreUsuario == usuario.nombreUsuario
            && u.Password == usuario.Password).FirstOrDefaultAsync();

            return user;

        }
    }
}
