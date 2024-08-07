﻿

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemadeComandas.Modelos
{
    public class PedidoCozinhaItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PedidoCozinhaId { get; set; }
        public virtual PedidoCozinha PedidoCozinha { get; set; }
        public int ComandaItemId { get; set; }
        public virtual ComandaItem ComandaItem { get; set; }

    }
}
