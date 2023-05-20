using ApiOAuthUsuarios.Data;
using ApiOAuthUsuarios.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiOAuthUsuarios.Repositories
{
    public class RepositoryUsuarios
    {
        private UsuariosContext context;

        public RepositoryUsuarios(UsuariosContext context)
        {
            this.context = context;
        }

        public async Task<Usuario> ExisteUsuario(string email, string password)
        {
            return await this.context.Usuarios.FirstOrDefaultAsync
                (x => x.Email == email && x.Password == password);

        }

        private async Task<int> GetMaxUserAsync()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.Usuarios.MaxAsync(x => x.IdUsuario) + 1;
            }
        }

        public async Task RegisterAsync(Usuario user)
        {
            Usuario newUser = new Usuario()
            {
                IdUsuario = await this.GetMaxUserAsync(),
                Nombre = user.Nombre,
                Email = user.Email,
                Password = user.Password,
                Imagen = user.Imagen
            };

            this.context.Add(newUser);
            await this.context.SaveChangesAsync();
        }

    }
}
