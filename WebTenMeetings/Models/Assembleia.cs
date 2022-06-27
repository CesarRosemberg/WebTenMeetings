using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebTenMeetings.Models
{
    [Table("Assembleia")]
    public class Assembleia
    {
        [Key]
        public int AssembleiaId { get; set; }
        
        public string Nome { get; set; }
        
        public bool Status { get; set; }
        
        public List<Participante> Participantes { get; set; }
       
        public List<Pauta> Pautas { get; set; }
    }
}
