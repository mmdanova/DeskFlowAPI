namespace Deskflow.Api.Models.Entities
{

    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public List<Chamado> Chamados { get; set; } = new();
    }
}