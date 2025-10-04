namespace Adset.Veiculos.Aplicacao.DTOs
{
    public class VeiculoOpcionalInput
    {
        public int IdItemOpicional { get; set; }
    }

    public class VeiculoPortalPacoteInput
    {
        public int Portal { get; set; }
        public int Pacote { get; set; }
    }

    public class VeiculoInputModel
    {
        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public int Ano { get; set; }
        public string Placa { get; set; } = null!;
        public int Km { get; set; }
        public string Cor { get; set; } = null!;
        public decimal Preco { get; set; }
        public List<VeiculoOpcionalInput>? Opcionais { get; set; }
        public List<VeiculoPortalPacoteInput>? PortalPacotes { get; set; }
    }
}