
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deskflow.Api.Models.Entities
{  
    public class Categoria
    {
        [Key]
        [Column("codCategoria")]
        public int Id { get; set; }
        
        [Required]
        [MaxLength(150)]
        [Column("nomeCategoria", TypeName ="varchar(150)") ]
        public string Nome { get; set; }

        public void Update (Categoria categoria)
        {
            Nome = categoria.Nome;
        }
    
    }
}