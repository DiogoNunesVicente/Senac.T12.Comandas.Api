﻿
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemadeComandas.Modelos
{
    public class PedidoCozinha
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int ComandaId { get; set; }
        public virtual Comanda Comanda { get; set; }
        public int SituacaoId { get; set; } = 1;
        public virtual ICollection<PedidoCozinhaItem> PedidoCozinhaItems { get; set; }

    }
}
