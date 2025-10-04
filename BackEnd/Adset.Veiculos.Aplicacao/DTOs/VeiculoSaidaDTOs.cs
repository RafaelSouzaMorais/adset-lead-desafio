namespace Adset.Veiculos.Aplicacao.DTOs
{
    public class VeiculoFotoDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
    public class VeiculoOpcionalDto
    {
        public int IdItemOpicional { get; set; }
        public string NomeItemOpicional { get; set; }
    }
    public class VeiculoPortalPacoteDto
    {
        public int Portal { get; set; }
        public string PortalNome { get; set; }
        public int Pacote { get; set; }
        public string PacoteNome { get; set; }
    }
    public class VeiculoSaidaDto
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public string Placa { get; set; }
        public int Km { get; set; }
        public string Cor { get; set; }
        public decimal Preco { get; set; }
        public List<VeiculoFotoDto> Fotos { get; set; }
        public List<VeiculoOpcionalDto> Opcionais { get; set; }
        public List<VeiculoPortalPacoteDto> PortalPacotes { get; set; }
    }
}