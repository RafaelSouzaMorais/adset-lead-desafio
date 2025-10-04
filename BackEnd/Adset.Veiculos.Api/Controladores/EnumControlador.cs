using Adset.Veiculos.Dominio.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Adset.Veiculos.Api.Controladores
{
    [ApiController]
    [Route("api/enum")]
    public class EnumControlador : Controller
    {
        [HttpGet]
        public IActionResult ObterEnums()
        {
            var portais = System.Enum.GetValues(typeof(PortalEnum))
                .Cast<PortalEnum>()
                .Select(e => new { valor = (int)e, nome = e.ToString() })
                .ToList();

            var pacotes = System.Enum.GetValues(typeof(PacoteEnum))
                .Cast<PacoteEnum>()
                .Select(e => new { valor = (int)e, nome = e.ToString() })
                .ToList();

            return Ok(new
            {
                portais,
                pacotes
            });
        }
    }
}
