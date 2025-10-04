namespace Adset.Veiculos.Dominio.Entidades
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Marca { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public int Ano { get; set; }
        public string Placa { get; set; } = null!;
        public int Km { get; set; }
        public string Cor { get; set; } = null!;
        public decimal Preco { get; set; }
        public ICollection<VeiculoFoto> Fotos { get; set; } = new List<VeiculoFoto>();
        public ICollection<VeiculoOpicional> Opcionais { get; set; } = new List<VeiculoOpicional>();
        public ICollection<VeiculoPortalPacote> PortalPacotes { get; set; } = new List<VeiculoPortalPacote>();
    }
}
