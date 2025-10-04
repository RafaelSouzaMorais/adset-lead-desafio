using Adset.Veiculos.Dominio.Enums;

namespace Adset.Veiculos.Dominio.Entidades
{
    public class VeiculoPortalPacote
    {
        public int IdVeiculo { get; set; }
        public Veiculo Veiculo { get; set; } = null!;
        public PortalEnum Portal { get; set; }
        public PacoteEnum Pacote { get; set; }
    }
}
