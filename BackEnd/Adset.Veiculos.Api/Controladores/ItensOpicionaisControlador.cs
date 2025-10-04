using Adset.Veiculos.Dominio.Entidades;
using Adset.Veiculos.Infraestrutura.Dados;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Adset.Veiculos.Api.Controladores
{
    [ApiController]
    [Route("api/itensopcionais")]
    public class ItensOpicionaisControlador : Controller
    {
        private readonly ApplicationDbContext _contexto;
        public ItensOpicionaisControlador(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> ObterItensOpicionais()
        {
            var itens = await _contexto.ItensOpicionais.ToListAsync();
            return Ok(itens);
        }
    }
}
