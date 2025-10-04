using Adset.Veiculos.Aplicacao.DTOs;
using Adset.Veiculos.Dominio.Entidades;
using Adset.Veiculos.Dominio.Enums;
using Adset.Veiculos.Infraestrutura.Dados;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Adset.Veiculos.Api.Controladores
{
    [ApiController]
    [Route("api/veiculo")]
    public class VeiculoControlador : Controller
    {
        private readonly ApplicationDbContext _contexto;
        public VeiculoControlador(ApplicationDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> ObterVeiculos()
        {
            var veiculos = await _contexto.Veiculos
                .Include(v => v.Fotos)
                .Include(v => v.Opcionais)
                    .ThenInclude(vo => vo.ItemOpicional)
                .Include(v => v.PortalPacotes)
                .Select(v => new VeiculoSaidaDto
                {
                    Id = v.Id,
                    Marca = v.Marca,
                    Modelo = v.Modelo,
                    Ano = v.Ano,
                    Placa = v.Placa,
                    Km = v.Km,
                    Cor = v.Cor,
                    Preco = v.Preco,
                    Fotos = v.Fotos.Select(f => new VeiculoFotoDto
                    {
                        Id = f.Id,
                        Url = f.Url
                    }).ToList(),
                    Opcionais = v.Opcionais.Select(o => new VeiculoOpcionalDto
                    {
                        IdItemOpicional = o.IdItemOpicional,
                        NomeItemOpicional = o.ItemOpicional != null ? o.ItemOpicional.Nome : null
                    }).ToList(),
                    PortalPacotes = v.PortalPacotes.Select(p => new VeiculoPortalPacoteDto
                    {
                        Portal = (int)p.Portal,
                        PortalNome = p.Portal.ToString(),
                        Pacote = (int)p.Pacote,
                        PacoteNome = p.Pacote.ToString()
                    }).ToList()
                }).ToListAsync();
            return Ok(veiculos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterVeiculoPorId(int id)
        {
            var veiculo = await _contexto.Veiculos
                .Include(v => v.Fotos)
                .Include(v => v.Opcionais)
                    .ThenInclude(vo => vo.ItemOpicional)
                .Include(v => v.PortalPacotes)
                .Select(v => new VeiculoSaidaDto
                {
                    Id = v.Id,
                    Marca = v.Marca,
                    Modelo = v.Modelo,
                    Ano = v.Ano,
                    Placa = v.Placa,
                    Km = v.Km,
                    Cor = v.Cor,
                    Preco = v.Preco,
                    Fotos = v.Fotos.Select(f => new VeiculoFotoDto
                    {
                        Id = f.Id,
                        Url = f.Url
                    }).ToList(),
                    Opcionais = v.Opcionais.Select(o => new VeiculoOpcionalDto
                    {
                        IdItemOpicional = o.IdItemOpicional,
                        NomeItemOpicional = o.ItemOpicional != null ? o.ItemOpicional.Nome : null
                    }).ToList(),
                    PortalPacotes = v.PortalPacotes.Select(p => new VeiculoPortalPacoteDto
                    {
                        Portal = (int)p.Portal,
                        PortalNome = p.Portal.ToString(),
                        Pacote = (int)p.Pacote,
                        PacoteNome = p.Pacote.ToString()
                    }).ToList()
                })
                .FirstOrDefaultAsync(v => v.Id == id);
            if (veiculo == null)
            {
                return NotFound();
            }
            return Ok(veiculo);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarVeiculo([FromBody] VeiculoInputModel veiculoJson)
        {
            if (veiculoJson == null)
            {
                return BadRequest("Dados do veículo são obrigatórios.");
            }

            try
            {
                var veiculo = new Veiculo
                {
                    Marca = veiculoJson.Marca,
                    Modelo = veiculoJson.Modelo,
                    Ano = veiculoJson.Ano,
                    Placa = veiculoJson.Placa,
                    Km = veiculoJson.Km,
                    Cor = veiculoJson.Cor,
                    Preco = veiculoJson.Preco
                };

                _contexto.Veiculos.Add(veiculo);
                await _contexto.SaveChangesAsync();

                // Opcionais
                if (veiculoJson.Opcionais != null && veiculoJson.Opcionais.Any())
                {
                    foreach (var opcional in veiculoJson.Opcionais)
                    {
                        var novoOpcional = new VeiculoOpicional
                        {
                            IdVeiculo = veiculo.Id,
                            IdItemOpicional = opcional.IdItemOpicional
                        };
                        _contexto.VeiculoOpicionais.Add(novoOpcional);
                    }
                }

                // PortalPacotes
                if (veiculoJson.PortalPacotes != null && veiculoJson.PortalPacotes.Any())
                {
                    foreach (var portalPacote in veiculoJson.PortalPacotes)
                    {
                        var existe = await _contexto.VeiculoPortalPacotes.AnyAsync(x =>
                            x.IdVeiculo == veiculo.Id &&
                            (int)x.Portal == portalPacote.Portal &&
                            (int)x.Pacote == portalPacote.Pacote);
                        if (!existe)
                        {
                            var novoPacote = new VeiculoPortalPacote
                            {
                                IdVeiculo = veiculo.Id,
                                Portal = (PortalEnum)portalPacote.Portal,
                                Pacote = (PacoteEnum)portalPacote.Pacote
                            };
                            _contexto.VeiculoPortalPacotes.Add(novoPacote);
                        }
                    }
                }

                await _contexto.SaveChangesAsync();

                var veiculoCompleto = await _contexto.Veiculos
                    .Include(v => v.Opcionais)
                        .ThenInclude(vo => vo.ItemOpicional)
                    .Include(v => v.PortalPacotes)
                    .FirstOrDefaultAsync(v => v.Id == veiculo.Id);

                return CreatedAtAction(nameof(ObterVeiculoPorId), new { id = veiculo.Id }, veiculoCompleto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao salvar veículo: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarVeiculo(int id, [FromBody] VeiculoInputModel veiculoJson)
        {
            if (veiculoJson == null)
            {
                return BadRequest("Dados do veículo são obrigatórios.");
            }

            var veiculoExistente = await _contexto.Veiculos
                .Include(v => v.Opcionais)
                .Include(v => v.PortalPacotes)
                .FirstOrDefaultAsync(v => v.Id == id);
            if (veiculoExistente == null)
            {
                return NotFound();
            }

            // Atualizar dados básicos
            veiculoExistente.Marca = veiculoJson.Marca;
            veiculoExistente.Modelo = veiculoJson.Modelo;
            veiculoExistente.Ano = veiculoJson.Ano;
            veiculoExistente.Placa = veiculoJson.Placa;
            veiculoExistente.Km = veiculoJson.Km;
            veiculoExistente.Cor = veiculoJson.Cor;
            veiculoExistente.Preco = veiculoJson.Preco;

            // Atualizar opcionais
            var opcionaisAntigos = _contexto.VeiculoOpicionais.Where(o => o.IdVeiculo == id);
            _contexto.VeiculoOpicionais.RemoveRange(opcionaisAntigos);
            if (veiculoJson.Opcionais != null && veiculoJson.Opcionais.Any())
            {
                foreach (var opcional in veiculoJson.Opcionais)
                {
                    var novoOpcional = new VeiculoOpicional
                    {
                        IdVeiculo = id,
                        IdItemOpicional = opcional.IdItemOpicional
                    };
                    _contexto.VeiculoOpicionais.Add(novoOpcional);
                }
            }

            // Atualizar portal pacotes
            var pacotesAntigos = await _contexto.VeiculoPortalPacotes.Where(p => p.IdVeiculo == id).ToListAsync();
            var pacotesNovos = veiculoJson.PortalPacotes ?? new List<VeiculoPortalPacoteInput>();

            // Remover os que não estão no JSON
            foreach (var antigo in pacotesAntigos)
            {
                bool existeNoJson = pacotesNovos.Any(novo =>
                    (int)novo.Portal == (int)antigo.Portal &&
                    (int)novo.Pacote == (int)antigo.Pacote);
                if (!existeNoJson)
                {
                    _contexto.VeiculoPortalPacotes.Remove(antigo);
                }
            }

            // Adicionar os que não existem no banco
            foreach (var novo in pacotesNovos)
            {
                bool existeNoBanco = pacotesAntigos.Any(antigo =>
                    (int)novo.Portal == (int)antigo.Portal &&
                    (int)novo.Pacote == (int)antigo.Pacote);
                if (!existeNoBanco)
                {
                    var novoPacote = new VeiculoPortalPacote
                    {
                        IdVeiculo = id,
                        Portal = (PortalEnum)novo.Portal,
                        Pacote = (PacoteEnum)novo.Pacote
                    };
                    _contexto.VeiculoPortalPacotes.Add(novoPacote);
                }
            }

            await _contexto.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarVeiculo(int id)
        {
            var veiculo = await _contexto.Veiculos.FindAsync(id);
            if (veiculo == null)
            {
                return NotFound();
            }
            _contexto.Veiculos.Remove(veiculo);
            await _contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
