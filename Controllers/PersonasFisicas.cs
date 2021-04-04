using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
//using PruebaDev.Controllers;
using PruebaDev.Models;
using PruebaDev.Models.ViewModel;
using PruebaDev.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.JsonPatch;

namespace PruebaDev.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PersonasFisicasController:ControllerBase
    {
        private readonly CursosCTX ctx;

        public PersonasFisicasController(CursosCTX _ctx){
            ctx = _ctx;
        }   

        [HttpGet]
        public async Task<IEnumerable<TbPersonasFisicas>> Get(){
            return await ctx.TbPersonasFisicas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id){
            
            var personafisica = await ctx.TbPersonasFisicas.FindAsync(id);
            if(personafisica == null){
                return NotFound();
            }
            else{
                return Ok();
            }            
        }
        [HttpPost]
        public async Task<IActionResult> Post(TbPersonasFisicas TbPersonasFisicas)
        {
            if(!ModelState.IsValid){
                return BadRequest(); //400
            }
            else
            {
                if(await ctx.TbPersonasFisicas.Where(x=>x.Rfc == TbPersonasFisicas.Rfc).AnyAsync()){
                    return BadRequest(ErrorHelper.Response(400, $"El codigo {TbPersonasFisicas.Rfc} ya existe"));
                }
                TbPersonasFisicas.IdPersonaFisica = 0;
                ctx.TbPersonasFisicas.Add(TbPersonasFisicas);
                await ctx.SaveChangesAsync();

                //return CreatedAtAction(nameof(Get), new { id = TbPersonasFisicas.IdPersonaFisica, Nombre=TbPersonasFisicas.Nombre}, TbPersonasFisicas); //Cambia URL
                return Created($"/TbPersonasFisicas/{TbPersonasFisicas.IdPersonaFisica}",TbPersonasFisicas);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TbPersonasFisicas TbPersonasFisicas)
        {
            if(TbPersonasFisicas.IdPersonaFisica == 0){
                TbPersonasFisicas.IdPersonaFisica = id;
            }
            if(TbPersonasFisicas.IdPersonaFisica != id){
                return BadRequest();
            }

            if(!await ctx.TbPersonasFisicas.Where(x=>x.IdPersonaFisica == id).AsNoTracking().AnyAsync()){
                return NotFound();
            }
            // if(await ctx.TbPersonasFisicas.Where(x=>x.Rfc == TbPersonasFisicas.Rfc && x.IdPersonaFisica != TbPersonasFisicas.IdPersonaFisica).AnyAsync()){
            //     return BadRequest(ErrorHelper.Response(400, $"El rfc {TbPersonasFisicas.Rfc} ya existe"));
            // }      

            ctx.Entry(TbPersonasFisicas).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("CambiarRFC/{id}")]//Extrae a partir de la URL example: https://localhost:5001/PersonasFisicas/CambiarRFC/2?rfc=GAMO920824SX9
        public async Task<IActionResult> CambiarRFC(int id, [FromQuery] string rfc)
        {
            if(string.IsNullOrWhiteSpace(rfc)){
                return BadRequest(ErrorHelper.Response(400, "El rfc esta vacio"));
            }
            var PersonaFisica = await ctx.TbPersonasFisicas.FindAsync(id);
            if(PersonaFisica == null){
                return NotFound();
            }

            if(await ctx.TbPersonasFisicas.Where(x=>x.Rfc == rfc && x.IdPersonaFisica != id).AnyAsync()){
                return BadRequest(ErrorHelper.Response(400, $"El rfc {rfc} ya existe"));
            }

            PersonaFisica.Rfc = rfc;
            await ctx.SaveChangesAsync();
            return StatusCode(204, PersonaFisica);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){
            var PersonaFisica = await ctx.TbPersonasFisicas.FindAsync(id);
            if(PersonaFisica == null){
                return NotFound();
            }

            ctx.TbPersonasFisicas.Remove(PersonaFisica);
            await ctx.SaveChangesAsync();
            return NoContent();
        }
        
    }
}