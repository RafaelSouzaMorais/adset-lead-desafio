namespace Adset.Veiculos.Dominio.Entidades
{
    public class ItemOpicional
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public ICollection<VeiculoOpicional> VeiculoOpicionais { get; set; } = new List<VeiculoOpicional>();
    }
}