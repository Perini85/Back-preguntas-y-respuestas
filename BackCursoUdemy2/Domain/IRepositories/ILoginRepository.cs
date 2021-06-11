using BackCursoUdemy2.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackCursoUdemy2.Domain.IRepositories
{
   public interface ILoginRepository
    {

        Task<Usuario> ValidateUser(Usuario usuario);



    }
}
