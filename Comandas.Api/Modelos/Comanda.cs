using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemadeComandas.Modelos
{
    public class Comanda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int NumeroMesa { get; set; }
        public string NomeCliente { get; set; }
        public int SituacaoComanda { get; set; } = 1;
        public virtual ICollection<ComandaItem> ComandaItems { get; set;}
    }
}
