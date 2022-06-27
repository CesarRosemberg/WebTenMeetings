using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTenMeetings.Models
{
    [Table("Participante")]
    
    public class Participante
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }
        public List<Pauta> PautasVotadas { get; set; }
    }
}
