namespace Adset.Veiculos.Dominio.Entidades
{
    public class VeiculoOpicional
    {
        public int IdVeiculo { get; set; }
        public Veiculo? Veiculo { get; set; }

        public int IdItemOpicional { get; set; }
        public ItemOpicional? ItemOpicional { get; set; }
    }
}