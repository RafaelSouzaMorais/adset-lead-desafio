namespace Adset.Veiculos.Dominio.Entidades
{ 
    public class VeiculoFoto
    {
        public int Id { get; set; }
        public int IdVeiculo { get; set; } 
        public Veiculo? Veiculo { get; set; }
        public string Url { get; set; }
    }
}
