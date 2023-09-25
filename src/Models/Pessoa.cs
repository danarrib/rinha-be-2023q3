using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RinhaBackend2023Q3.Models
{
    [Index(nameof(Apelido), IsUnique = true)]
    [Index(nameof(SearchIndex), IsUnique = false)]
    public class Pessoa
    {
        public Guid Id { get; set; } // UUID v4 has 32 hex chars (128 bits), plus 4 dashes

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(32)]
        public string Apelido { get; set; }

        [Required]
        [StringLength(10)]
        public string Nascimento { get; set; } // YYYY-MM-DD
        
        [Column(TypeName = "jsonb")]
        public List<string> Stack { get; set; }

        [JsonIgnore]
        public string SearchIndex { get; set; }
    }

}
