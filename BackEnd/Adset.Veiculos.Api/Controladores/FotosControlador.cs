using Adset.Veiculos.Dominio.Entidades;
using Adset.Veiculos.Infraestrutura.Dados;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Adset.Veiculos.Api.Controladores
{
    [ApiController]
    [Route("api/foto")]
    public class FotosControlador : Controller
    {
        private readonly ApplicationDbContext _contexto;
        public FotosControlador(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpPost("veiculo/{idVeiculo}")]
        public async Task<IActionResult> AdicionarFotos(int idVeiculo, [FromForm] List<IFormFile> arquivos)
        {
            if (arquivos == null || !arquivos.Any())
            {
                return BadRequest("Nenhum arquivo enviado.");
            }

            if (arquivos.Count > 15)
            {
                return BadRequest("O número máximo de fotos é 15.");
            }

            var veiculo = await _contexto.Veiculos.Include(v => v.Fotos).FirstOrDefaultAsync(v => v.Id == idVeiculo);
            if (veiculo == null)
            {
                return NotFound();
            }

            if (veiculo.Fotos.Count + arquivos.Count > 15)
            {
                return BadRequest("O número total de fotos não pode exceder 15.");
            }

            string diretorioFotos = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "fotos");
            if (!Directory.Exists(diretorioFotos))
            {
                Directory.CreateDirectory(diretorioFotos);
            }

            foreach (var arquivo in arquivos)
            {
                if (arquivo.Length > 0)
                {
                    //tratamento para nome unico
                    string nomeArquivo = $"{Guid.NewGuid()}{Path.GetExtension(arquivo.FileName)}";
                    string caminhoArquivo = Path.Combine(diretorioFotos, nomeArquivo);

                    using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                    {
                        await arquivo.CopyToAsync(stream);
                    }

                    veiculo.Fotos.Add(new VeiculoFoto
                    {
                        Url = $"/fotos/{nomeArquivo}",
                        IdVeiculo = idVeiculo,
                    });
                }
            }

            try
            {
                _contexto.Veiculos.Add(veiculo);
                await _contexto.SaveChangesAsync();

                //// Adicionar opcionais e pacotes, se existirem
                //if (veiculo.Opcionais != null)
                //{
                //    foreach (var opcional in veiculo.Opcionais)
                //    {
                //        opcional.IdVeiculo = veiculo.Id;
                //        _contexto.VeiculoOpicionais.Add(opcional);
                //    }
                //}
                //if (veiculo.PortalPacotes != null)
                //{
                //    foreach (var pacote in veiculo.PortalPacotes)
                //    {
                //        pacote.IdVeiculo = veiculo.Id;
                //        _contexto.VeiculoPortalPacotes.Add(pacote);
                //    }
                //}
                //await _contexto.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao salvar fotos: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverFoto(int id)
        {
            var foto = await _contexto.VeiculoFotos.FindAsync(id);
            if (foto == null)
            {
                return NotFound();
            }
            string caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", foto.Url.TrimStart('/'));
            if (System.IO.File.Exists(caminhoArquivo))
            {
                System.IO.File.Delete(caminhoArquivo);
            }
            _contexto.VeiculoFotos.Remove(foto);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("mult")]
        public async Task<IActionResult> RemoverFotos([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return BadRequest("Nenhum ID de foto fornecido.");
            }
            var fotos = await _contexto.VeiculoFotos.Where(f => ids.Contains(f.Id)).ToListAsync();
            if (fotos.Count == 0)
            {
                return NotFound();
            }

            foreach (var foto in fotos)
            {
                string caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", foto.Url.TrimStart('/'));
                if (System.IO.File.Exists(caminhoArquivo))
                {
                    System.IO.File.Delete(caminhoArquivo);
                }
                _contexto.VeiculoFotos.Remove(foto);
            }
            await _contexto.SaveChangesAsync();
            return NoContent();
        }

    }
}
