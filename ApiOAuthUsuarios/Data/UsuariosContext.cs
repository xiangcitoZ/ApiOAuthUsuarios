using ApiOAuthTutorial.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiOAuthUsuarios.Data
{
    public class UsuariosContext: DbContext
    {
        public UsuariosContext
            (DbContextOptions<UsuariosContext> options)
        :base(options){ } 

        public DbSet<Usuario> Usuarios { get; set; }    
    }
}
