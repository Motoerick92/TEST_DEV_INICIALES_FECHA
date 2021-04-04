using System.Linq;
using System.Threading.Tasks;
using PruebaDev.Helper;
using PruebaDev.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaDev.Models.ViewModel;

namespace PruebaDev.Controllers
{
        [ApiController]
        [Route("[controller]")]
        public class UsuariosController:Controller{
            CursosCTX ctx;
            public UsuariosController(CursosCTX _ctx){
                ctx = _ctx;
            }   

            [HttpPost]
            public async Task<IActionResult> Post(Usuarios Usuario){
                if(!ModelState.IsValid){
                    return BadRequest(ErrorHelper.GetModelStateErrors(ModelState));
                }

                if(await ctx.Usuarios.Where(x=>x.Usuario == Usuario.Usuario).AnyAsync()){
                    return BadRequest(ErrorHelper.Response(400, $"El usuario {Usuario.Usuario} ya existe"));
                }
           

            HashedPassword Password = HashHelper.Hash(Usuario.Clave);
            Usuario.Clave = Password.Password;
            Usuario.Sal = Password.Salt;
            ctx.Usuarios.Add(Usuario);
            await ctx.SaveChangesAsync();
            return Ok(new UsuarioVM(){
                IdUsuario = Usuario.IdUsuario,
                Usuario = Usuario.Usuario
            });
        }

    }
}
