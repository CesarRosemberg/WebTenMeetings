using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTenMeetings.Models
{
    [Table("Pauta")]
    public class Pauta
    {
        [Key]
        public int PautaId { get; set; }
        public string Nome { get; set; }
        public int QtdTotalVotos { get; set; }
        public List<Participante> Participantes { get; set; }
    }
}
